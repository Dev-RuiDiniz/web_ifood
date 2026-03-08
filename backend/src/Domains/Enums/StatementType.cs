using System;

namespace Domains.Enums
{
    public enum StatementType
    {
        OrderCredit,       // Credit to store for items sold
        DeliveryFeeCredit, // Credit to courier for delivery
        CommissionDebit,    // Platform fee taken from store
        RefundDebit,        // When money is returned to customer
        PayoutDebit         // When the platform actually pays the store/courier
    }
}
