using System;
using Domains.Base;

namespace Domains.Entities
{
    public class AuditLog : BaseEntity
    {
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }
        
        public string Action { get; set; } = string.Empty; // Login, Logout, UpdateProfile, etc.
        public string IpAddress { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
