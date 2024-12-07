using SecureDataManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace SecureDataManagement.Data
{
    public class SecureDbContext : DbContext
    {
        public SecureDbContext(DbContextOptions<SecureDbContext> options) : base(options) { }

        public DbSet<EncryptedData>? EncryptedDatas { get; set; }
        public DbSet<DecryptedData>? DecryptedDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SecureDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
