using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLib
{
    public static class Hmac
    {
        public static string Gen(string input, string key)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] digestBytes;

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key))) {
                digestBytes = hmac.ComputeHash(inputBytes);
            }
            return Convert.ToBase64String(digestBytes);
        }

        public static bool Verify(string input, string digest, string key)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] digestBytes;

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key))) {
                digestBytes = hmac.ComputeHash(inputBytes);
            }
            string calculatedDigest = Convert.ToBase64String(digestBytes);
            Console.WriteLine();
            Console.WriteLine("Calculated HMAC: {0}", calculatedDigest);

            return digest.Equals(calculatedDigest, StringComparison.Ordinal);
        }
    }
}
