using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLib
{
    public class Asymmetric
    {
        private RSACryptoServiceProvider _provider;

        public Asymmetric()
        {
            _provider = new RSACryptoServiceProvider();
        }

        public byte[] Encrypt(string plaintext, string publicKey)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
            _provider.FromXmlString(publicKey);
            byte[] cipherBytes = _provider.Encrypt(plainBytes, true);
            return cipherBytes;
        }

        public byte[] Decrypt(string ciphertext, string privateKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(ciphertext);
            _provider.FromXmlString(privateKey);
            byte[] clearBytes = _provider.Decrypt(cipherBytes, true);         
            return clearBytes;
        }

        public string[] GenerateKeyPair()
        {
            string[] keys = new string[2];
            keys[0] = _provider.ToXmlString(false);
            keys[1] = _provider.ToXmlString(true);
            return keys;
        }
    }
}
