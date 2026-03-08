using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ChatMessageMapping : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessages");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(c => c.Order)
                .WithMany()
                .HasForeignKey(c => c.OrderId);

            builder.HasOne(c => c.Sender)
                .WithMany()
                .HasForeignKey(c => c.SenderId);
        }
    }
}
