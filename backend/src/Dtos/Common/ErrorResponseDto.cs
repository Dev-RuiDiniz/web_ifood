using System;

namespace Dtos.Common
{
    public class ErrorResponseDto
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Message { get; set; } = string.Empty;
        public string? TraceId { get; set; }
    }
}
