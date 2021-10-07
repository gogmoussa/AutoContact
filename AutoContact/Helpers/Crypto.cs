using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;


namespace AutoContact.Helpers
{
    public class Crypto
    {
        public static string GenerateSHA512String(string s)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }

        public static string generateSalt()
        {
            int HASH_SALT_LENGTH = 24;
            byte[] saltBytes = new byte[HASH_SALT_LENGTH];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltBytes);

            string salt = byteArrayToString(saltBytes);
            return salt;
        }

        private static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");

            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }

            return output.ToString();
        }

        public static string hashPassword(string plaintextPassword, string salt)
        {
            return Helpers.Crypto.GenerateSHA512String(plaintextPassword + salt);
        }
    }
}
