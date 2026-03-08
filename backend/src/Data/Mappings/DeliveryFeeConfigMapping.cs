using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domains.Entities;

namespace Data.Mappings
{
    public class DeliveryFeeConfigMapping : IEntityTypeConfiguration<DeliveryFeeConfig>
    {
        public void Configure(EntityTypeBuilder<DeliveryFeeConfig> builder)
        {
            builder.ToTable("DeliveryFeeConfigs");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Fee)
                .HasPrecision(18, 2);

            builder.Property(d => d.FreeShippingThreshold)
                .HasPrecision(18, 2);

            builder.HasOne(d => d.Restaurant)
                .WithMany()
                .HasForeignKey(d => d.RestaurantId);
        }
    }
}
