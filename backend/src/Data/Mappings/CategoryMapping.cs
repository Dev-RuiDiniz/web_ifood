using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domains.Entities;

namespace Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.Name)
                .IsUnique();
        }
    }

    public class RestaurantCategoryMapping : IEntityTypeConfiguration<RestaurantCategory>
    {
        public void Configure(EntityTypeBuilder<RestaurantCategory> builder)
        {
            builder.ToTable("RestaurantCategories");

            builder.HasKey(rc => new { rc.RestaurantId, rc.CategoryId });

            builder.HasOne(rc => rc.Restaurant)
                .WithMany()
                .HasForeignKey(rc => rc.RestaurantId);

            builder.HasOne(rc => rc.Category)
                .WithMany(c => c.RestaurantCategories)
                .HasForeignKey(rc => rc.CategoryId);
        }
    }
}
