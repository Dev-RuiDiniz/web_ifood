using Dtos.Payments;
using Domains.Entities;
using QRCoder;
using System;
using System.Threading.Tasks;

namespace Services.Payments
{
    public interface IPaymentProvider
    {
        Task<PixResponseDto> GeneratePixAsync(Order order);
    }

    public class PixPaymentService : IPaymentProvider
    {
        public Task<PixResponseDto> GeneratePixAsync(Order order)
        {
            // Mocking a payload that would come from a Gateway
            var payload = $"00020126330014BR.GOV.BCB.PIX0111123456789015204000053039865405{order.TotalAmount:F2}5802BR5913WEB_IFOOD_APP6009SAO_PAULO62070503***6304";
            
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(20);
            
            var base64QrCode = Convert.ToBase64String(qrCodeBytes);

            return Task.FromResult(new PixResponseDto
            {
                CopyAndPaste = payload,
                QrCodeBase64 = base64QrCode,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                OrderNumber = order.OrderNumber
            });
        }
    }
}
