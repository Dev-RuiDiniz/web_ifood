using System;
using Domains.Base;

namespace Domains.Entities
{
    public class ChatMessage : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}
