using System;
using Domains.Base;

namespace Domains.Entities
{
    public class OtpCode : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        
        public string Code { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public string Type { get; set; } = "Login"; // Login, ResetPassword, ConfirmEmail
    }
}
