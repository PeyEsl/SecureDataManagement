using SecureDataManagement.Application.Interfaces;
using SecureDataManagement.Application.Services;
using SecureDataManagement.Core.Interfaces;
using SecureDataManagement.Data.Repositories;

namespace SecureDataManagement.Web.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDIScoped(this IServiceCollection services)
        {
            services.AddScoped<IEncryptedDataRepository, EncryptedDataRepository>();
            services.AddScoped<IDecryptedDataRepository, DecryptedDataRepository>();

            services.AddScoped<IAESService, AESService>();
            services.AddScoped<IRSAService, RSAService>();
            services.AddScoped<IHashingService, HashingService>();
            services.AddScoped<IKeyDerivationService, KeyDerivationService>();
            services.AddScoped<IDataProtectionService, DataProtectionService>();
            services.AddScoped<IHMACService, HMACService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IDecryptionService, DecryptionService>();

            return services;
        }
    }
}
