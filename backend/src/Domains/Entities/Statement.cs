using System;
using Domains.Base;
using Domains.Enums;

namespace Domains.Entities
{
    public class Statement : BaseEntity
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        
        public Guid WalletId { get; set; } // Refers to User.Id (Store owner or Courier)
        public virtual User WalletOwner { get; set; } = null!;

        public decimal Amount { get; set; }
        public string Type { get; set; } = StatementType.OrderCredit.ToString();
        public string Description { get; set; } = string.Empty;
        
        public bool IsSettled { get; set; } = false; // Whether the actual Payout has happened
        public DateTime? SettledAt { get; set; }
    }
}
