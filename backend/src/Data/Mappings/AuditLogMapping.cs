using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domains.Entities;

namespace Data.Mappings
{
    public class AuditLogMapping : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.IpAddress)
                .HasMaxLength(50);

            builder.Property(a => a.UserAgent)
                .HasMaxLength(500);

            builder.Property(a => a.Details)
                .HasMaxLength(2000);

            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .IsRequired(false);
        }
    }
}
