using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domains.Entities;

namespace Data.Mappings
{
    public class ReviewMapping : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Stars)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(1000);

            builder.HasOne(r => r.Order)
                .WithMany()
                .HasForeignKey(r => r.OrderId);

            builder.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.Restaurant)
                .WithMany()
                .HasForeignKey(r => r.RestaurantId);
            
            // Uniqueness per order
            builder.HasIndex(r => r.OrderId).IsUnique();
        }
    }
}
