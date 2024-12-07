using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureDataManagement.Core.Entities;

namespace SecureDataManagement.Data.Configurations
{
    public class EncryptedDataConfiguration : IEntityTypeConfiguration<EncryptedData>
    {
        public void Configure(EntityTypeBuilder<EncryptedData> builder)
        {
            builder.ToTable("EncryptedDatas");

            builder.HasKey(ed => ed.Id);

            builder.Property(ed => ed.PlainText)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(ed => ed.EncryptedText)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(ed => ed.IV)
                   .HasMaxLength(50) 
                   .IsRequired(false);
            
            builder.Property(ed => ed.PublicKey)
                   .IsRequired(false);
            
            builder.Property(ed => ed.DecryptKey)
                   .IsRequired(false);
            
            builder.Property(ed => ed.Algorithm)
                   .HasMaxLength(20) 
                   .IsRequired();

            builder.Property(ed => ed.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
