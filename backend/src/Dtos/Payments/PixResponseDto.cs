using System;

namespace Dtos.Payments
{
    public class PixResponseDto
    {
        public string CopyAndPaste { get; set; } = string.Empty;
        public string QrCodeBase64 { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
    }
}
