using SecureDataManagement.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SecureDataManagement.Application.Services
{
    public class AESService : IAESService
    {
        /// <summary>
        /// رمزنگاری متن ورودی با استفاده از AES.
        /// </summary>
        public (string EncryptedText, string IV, string Key) Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateKey(); // تولید کلید
                aes.GenerateIV();  // تولید IV

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(cs, Encoding.UTF8))
                        {
                            writer.Write(plainText);
                        }
                    }

                    var encryptedBytes = ms.ToArray();
                    var encryptedText = Convert.ToBase64String(encryptedBytes);
                    var iv = Convert.ToBase64String(aes.IV);
                    var key = Convert.ToBase64String(aes.Key);

                    return (encryptedText, iv, key);
                }
            }
        }

        /// <summary>
        /// رمزگشایی متن رمزنگاری‌شده با استفاده از AES.
        /// </summary>
        public string Decrypt(string encryptedText, string iv, string key)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var keyBytes = Convert.FromBase64String(key);
            var ivBytes = Convert.FromBase64String(iv);

            using (var aes = Aes.Create())
            {
                var decryptor = aes.CreateDecryptor(keyBytes, ivBytes);

                using (var ms = new MemoryStream(encryptedBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cs, Encoding.UTF8))
                        {
                            var decryptedText = reader.ReadToEnd();
                            return decryptedText;
                        }
                    }
                }
            }
        }
    }
}
