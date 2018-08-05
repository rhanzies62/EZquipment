using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Ems.Service.Cryptography
{
    public static class Cryptography
    {
        private const string _numChars = "0123456789";
        private const string _alphaChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static string RandomNumberValue(int length)
        {
            return GenerateRandomValue(_numChars, length);
        }

        public static string RandomAlphaNumericValue(int length)
        {
            return GenerateRandomValue(_alphaChars + _numChars, length);
        }

        public static string RandomAlphaValue(int length)
        {
            return GenerateRandomValue(_alphaChars, length);
        }

        public static string HashString(string value, string salt)
        {
            SHA256 mySHA256 = SHA256Cng.Create();
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            byte[] DataToEncrypt = encoding.GetBytes(value + salt);
            return Convert.ToBase64String(mySHA256.ComputeHash(DataToEncrypt));
        }

        public static string HashPassword(string value)
        {
            return HashString(value, "asdNI131+");
        }

        public static string CreateSalt()
        {
            byte[] randomNumber = new byte[16];
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();

            rngCsp.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static KeyPair CreateKeyPairs()
        {
            KeyPair output = new KeyPair();

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            output.Private = rsa.ToXmlString(true);
            output.Public = rsa.ToXmlString(false);

            return output;
        }

        private static string GenerateRandomValue(string characters, int length)
        {
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(characters, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }
    }
}
