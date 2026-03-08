using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domains.Entities;

namespace Data.Mappings
{
    public class CourierMapping : IEntityTypeConfiguration<Courier>
    {
        public void Configure(EntityTypeBuilder<Courier> builder)
        {
            builder.ToTable("Couriers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.VehicleType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LicenseNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);
            
            // Uniqueness per User
            builder.HasIndex(c => c.UserId).IsUnique();
        }
    }
}
