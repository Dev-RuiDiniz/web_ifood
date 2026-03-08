using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domains.Entities;

namespace Data.Mappings
{
    public class RestaurantMapping : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurants");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.CommercialName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(r => r.LegalName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.CNPJ)
                .IsRequired()
                .HasMaxLength(14);

            builder.HasIndex(r => r.CNPJ)
                .IsUnique();

            builder.Property(r => r.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Address)
                .WithMany()
                .HasForeignKey(r => r.AddressId);
        }
    }
}
