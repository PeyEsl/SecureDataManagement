using SecureDataManagement.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SecureDataManagement.Application.Services
{
    public class RSAService : IRSAService
    {
        #region Ctor

        private readonly RSA _rsa;

        public RSAService()
        {
            _rsa = RSA.Create();
        }

        #endregion

        /// <summary>
        /// تولید کلید عمومی و کلید خصوصی.
        /// </summary>
        public (string PublicKey, string PrivateKey) GenerateKeys()
        {
            // استخراج کلیدها به فرمت XML
            var publicKey = _rsa.ToXmlString(false); // فقط کلید عمومی
            var privateKey = _rsa.ToXmlString(true); // کلید عمومی و خصوصی
            return (publicKey, privateKey);
        }

        /// <summary>
        /// رمزنگاری متن ورودی با استفاده از RSA.
        /// </summary>
        public string Encrypt(string plainText, string publicKey)
        {
            // وارد کردن کلید عمومی
            _rsa.FromXmlString(publicKey);

            // رمزنگاری
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = _rsa.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA256);
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// رمزگشایی متن رمزنگاری‌شده با استفاده از RSA.
        /// </summary>
        public string Decrypt(string encryptedText, string privateKey)
        {
            // وارد کردن کلید خصوصی
            _rsa.FromXmlString(privateKey);

            // رمزگشایی
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var plainBytes = _rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
