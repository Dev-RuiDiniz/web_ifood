using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domains.Entities;

namespace Data.Mappings
{
    public class StatementMapping : IEntityTypeConfiguration<Statement>
    {
        public void Configure(EntityTypeBuilder<Statement> builder)
        {
            builder.ToTable("Statements");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(s => s.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(s => s.Order)
                .WithMany()
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.WalletOwner)
                .WithMany()
                .HasForeignKey(s => s.WalletId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
