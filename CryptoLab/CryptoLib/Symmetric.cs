using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLib
{
    public class Symmetric
    {
        private readonly RijndaelManaged _provider;

        private byte[] _key { get; }
        public byte[] _iv { get; }

        public Symmetric(string key, string iv)
        {
            _provider = new RijndaelManaged();
            _key = Convert.FromBase64String(key);
            _iv = Convert.FromBase64String(iv);
        }

        public byte[] Encrypt(string plaintext)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] cipherBytes;

            using (var buf = new MemoryStream()) {
                using (var stream = new CryptoStream(buf, _provider.CreateEncryptor(_key, _iv), CryptoStreamMode.Write)) {
                    stream.Write(clearBytes, 0, clearBytes.Length);
                    stream.FlushFinalBlock();
                }
                cipherBytes = buf.ToArray();
            }
            return cipherBytes;
        }

        public byte[] Decrypt(string ciphertext)
        {
            byte[] cipherBytes = Convert.FromBase64String(ciphertext);
            byte[] clearBytes;

            using (var buf = new MemoryStream()) {
                using (var stream = new CryptoStream(buf, _provider.CreateDecryptor(_key, _iv), CryptoStreamMode.Write)) {
                    stream.Write(cipherBytes, 0, cipherBytes.Length);
                    stream.FlushFinalBlock();
                }
                clearBytes = buf.ToArray();
            }
            return clearBytes;
        }

        public string GenerateKey()
        {
            byte[] keyBytes;
            using (var rngProvider = new RNGCryptoServiceProvider()) {
                keyBytes = new byte[_provider.KeySize / 8];
                rngProvider.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }

        public string GenerateIV()
        {
            byte[] ivBytes;
            using (var rngProvider = new RNGCryptoServiceProvider()) {
                ivBytes = new byte[_provider.BlockSize / 8];
                rngProvider.GetBytes(ivBytes);
            }
            return Convert.ToBase64String(ivBytes);
        }
    }
}
