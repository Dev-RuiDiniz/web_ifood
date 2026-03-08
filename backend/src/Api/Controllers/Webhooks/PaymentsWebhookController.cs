using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.UpdateOrderStatus;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Controllers.Webhooks
{
    [ApiController]
    [Route("api/webhooks/payments")]
    public class PaymentsWebhookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string WebhookSecret = "super-secret-key"; // In reality, this would be in appsettings/vault

        public PaymentsWebhookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            // Validate signature
            if (!Request.Headers.TryGetValue("X-Payment-Signature", out var signature) || signature != WebhookSecret)
            {
                return Unauthorized();
            }

            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var payload = JsonSerializer.Deserialize<PaymentNotification>(body);

            if (payload == null) return BadRequest();

            if (payload.Type == "payment.success")
            {
                var command = new UpdateOrderStatusCommand
                {
                    OrderId = payload.OrderId,
                    NewStatus = "Confirmed"
                };

                await _mediator.Send(command);
            }

            return Ok();
        }

        public class PaymentNotification
        {
            public string Type { get; set; } = string.Empty;
            public Guid OrderId { get; set; }
            public string Status { get; set; } = string.Empty;
        }
    }
}
