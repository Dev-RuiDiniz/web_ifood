using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domains.Entities;

namespace Data.Mappings
{
    public class TermConsentMapping : IEntityTypeConfiguration<TermConsent>
    {
        public void Configure(EntityTypeBuilder<TermConsent> builder)
        {
            builder.ToTable("TermConsents");

            builder.HasKey(tc => tc.Id);

            builder.Property(tc => tc.TermVersion)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(tc => tc.ConsentedAt)
                .IsRequired();

            builder.HasOne(tc => tc.User)
                .WithMany(u => u.TermConsents)
                .HasForeignKey(tc => tc.UserId);
        }
    }
}
