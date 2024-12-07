using Microsoft.AspNetCore.DataProtection;
using SecureDataManagement.Application.Interfaces;

namespace SecureDataManagement.Application.Services
{
    public class DataProtectionService : IDataProtectionService
    {
        #region Ctor

        private readonly IDataProtector _protector;

        public DataProtectionService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("SensitiveDataProtector");
        }

        #endregion

        /// <summary>
        /// رمزنگاری داده‌ها
        /// </summary>
        public string Protect(string plainText)
        {
            return _protector.Protect(plainText);
        }

        /// <summary>
        /// رمزگشایی داده‌ها
        /// </summary>
        public string Unprotect(string protectedText)
        {
            try
            {
                return _protector.Unprotect(protectedText);
            }
            catch
            {
                return "Invalid or tampered data";
            }
        }
    }
}
