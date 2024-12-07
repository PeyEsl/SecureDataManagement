using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureDataManagement.Core.Entities;

namespace SecureDataManagement.Data.Configurations
{
    public class DecryptedDataConfiguration : IEntityTypeConfiguration<DecryptedData>
    {
        public void Configure(EntityTypeBuilder<DecryptedData> builder)
        {
            builder.ToTable("DecryptedDatas");

            builder.HasKey(dd => dd.Id);

            builder.Property(dd => dd.PlainText)
                   .HasMaxLength(200)
                   .IsRequired(false);

            builder.Property(dd => dd.DecryptedText)
                   .HasMaxLength(250)
                   .IsRequired(false);

            builder.Property(dd => dd.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(dd => dd.EncryptedData)
                   .WithMany()
                   .HasForeignKey(dd => dd.EncryptedDataId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
