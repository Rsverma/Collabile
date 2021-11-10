using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Collabile.Api.Helpers
{
    public class EncryptionHelper
    {
        private const int ITERATIONCOUNT = 10000;
        private const int BYTECOUNT = 20;
        
        public static string Encrypt(string password)
        {
            string hashPass = string.Empty;
            if (!string.IsNullOrEmpty(password))
            {
                byte[] salt;
                RandomNumberGenerator.Create().GetBytes(salt = new byte[BYTECOUNT]);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONCOUNT);
                byte[] hash = pbkdf2.GetBytes(BYTECOUNT);

                byte[] hashBytes = new byte[BYTECOUNT * 2];
                Array.Copy(salt, 0, hashBytes, 0, BYTECOUNT);
                Array.Copy(hash, 0, hashBytes, BYTECOUNT, BYTECOUNT);

                hashPass = Convert.ToBase64String(hashBytes);
            }
            return hashPass;
        }

        public static bool Validate(string password, string hashPass)
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(hashPass))
            {
                byte[] hashBytes = Convert.FromBase64String(hashPass);
                byte[] salt = new byte[BYTECOUNT];
                Array.Copy(hashBytes, 0, salt, 0, BYTECOUNT);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONCOUNT);
                byte[] hash = pbkdf2.GetBytes(BYTECOUNT);

                for (int i = 0; i < BYTECOUNT; i++)
                {
                    if (hashBytes[i + BYTECOUNT] != hash[i])
                    {
                        return false;
                    }
                }
                isValid = true;
            }
            return isValid;
        }
    }
}