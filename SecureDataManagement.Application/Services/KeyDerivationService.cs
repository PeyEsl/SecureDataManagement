using SecureDataManagement.Application.Interfaces;
using System.Security.Cryptography;

namespace SecureDataManagement.Application.Services
{
    public class KeyDerivationService : IKeyDerivationService
    {
        #region GenerateSalt

        private const int SaltSize = 16; // اندازه Salt (بایت)
        private const int KeySize = 32; // اندازه کلید (بایت)
        private const int Iterations = 100000; // تعداد دفعات اجرا

        /// <summary>
        /// تولید Salt به صورت تصادفی
        /// </summary>
        private static byte[] GenerateSalt()
        {
            var salt = new byte[SaltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        #endregion

        /// <summary>
        /// تولید کلید از رمز عبور با استفاده از PBKDF2
        /// </summary>
        public (string DerivedKey, string Salt) GenerateKey(string password)
        {
            var salt = GenerateSalt();
            using var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = deriveBytes.GetBytes(KeySize);

            return (Convert.ToBase64String(key), Convert.ToBase64String(salt));
        }

        /// <summary>
        /// تأیید رمز عبور با استفاده از کلید و Salt ذخیره شده
        /// </summary>
        public bool VerifyKey(string password, string storedKey, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            using var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var key = deriveBytes.GetBytes(KeySize);

            return Convert.ToBase64String(key) == storedKey;
        }
    }
}
