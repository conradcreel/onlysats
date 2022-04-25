using System;
using System.Security.Cryptography;
using System.Text;

namespace onlysats.domain.Services
{
    public static class HashService
    {
        public static string HashSHA256(string input)
        {
            using var hash = SHA256.Create();
            var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(byteArray).ToLower();
        }
    }
}