using SecureDataManagement.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SecureDataManagement.Application.Services
{
    public class HMACService : IHMACService
    {
        private byte[]? _key;

        /// <summary>
        /// تنظیم کلید HMAC.
        /// </summary>
        public void SetKey(string base64Key)
        {
            _key = Convert.FromBase64String(base64Key);
        }

        /// <summary>
        /// تولید امضای دیجیتال برای داده‌ها
        /// </summary>
        public string GenerateSignature(string data)
        {
            if (_key == null)
            {
                throw new InvalidOperationException("HMAC key is not set. Please call SetKey before using this method.");
            }

            using var hmac = new HMACSHA256(_key);
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var signatureBytes = hmac.ComputeHash(dataBytes);
            return Convert.ToBase64String(signatureBytes);
        }

        /// <summary>
        /// تأیید امضای دیجیتال
        /// </summary>
        public bool VerifySignature(string data, string signature)
        {
            var generatedSignature = GenerateSignature(data);
            return generatedSignature == signature;
        }

        /// <summary>
        /// تولید کلید تصادفی.
        /// </summary>
        public static string GenerateRandomKey()
        {
            using var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }
    }
}
