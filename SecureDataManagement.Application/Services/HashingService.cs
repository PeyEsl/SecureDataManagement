using SecureDataManagement.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SecureDataManagement.Application.Services
{
    public class HashingService : IHashingService
    {
        /// <summary>
        /// تولید هش از متن ورودی با استفاده از الگوریتم SHA256
        /// </summary>
        public string GenerateHash(string input)
        {
            using var sha256 = SHA256.Create();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// مقایسه یک متن با هش موجود
        /// </summary>
        public bool VerifyHash(string input, string hash)
        {
            var computedHash = GenerateHash(input);
            return computedHash == hash;
        }
    }
}
