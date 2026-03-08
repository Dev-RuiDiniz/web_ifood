using Domains.Entities;
using System.Threading.Tasks;

namespace Services.Payments
{
    public interface IRefundService
    {
        Task<bool> RefundOrderAsync(Order order);
    }

    public class MockRefundService : IRefundService
    {
        public Task<bool> RefundOrderAsync(Order order)
        {
            // Simulate calling the Gateway's refund API (Stripe, Mercado Pago...)
            // In PIX, this would use a specific refund endpoint from the Central Bank / PSP
            System.Diagnostics.Debug.WriteLine($"Simulating refund for order {order.OrderNumber} of amount {order.TotalAmount:C}");
            return Task.FromResult(true); // Always succeeds in Mock
        }
    }
}
