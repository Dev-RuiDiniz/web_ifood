using System;
using Domains.Base;

namespace Domains.Entities
{
    public class TermConsent : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        
        public string TermVersion { get; set; } = string.Empty;
        public DateTime ConsentedAt { get; set; } = DateTime.UtcNow;
        public bool IsAccepted { get; set; }
    }
}
