using System.Collections.Generic;
using Domains.Base;
using Domains.Enums;

namespace Domains.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty; // CPF or CNPJ
        public string Phone { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Customer;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public virtual ICollection<TermConsent> TermConsents { get; set; } = new List<TermConsent>();
    }
}
