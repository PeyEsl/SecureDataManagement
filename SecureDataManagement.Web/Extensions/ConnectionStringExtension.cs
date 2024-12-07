using Microsoft.EntityFrameworkCore;
using SecureDataManagement.Data;

namespace SecureDataManagement.Web.Extensions
{
    public static class ConnectionStringExtension
    {
        public static IServiceCollection AddConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            // Get ConnectionString from AppSetting
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'ThrillConnectionString' not found.");

            // Configure DbContext
            services.AddDbContext<SecureDbContext>(options =>
                                                   options.UseSqlServer(connectionString));

            // Add developer page exception filter
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
