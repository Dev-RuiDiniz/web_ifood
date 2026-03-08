using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.AddReview;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId!);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddReviewRequest request)
        {
            var command = new AddReviewCommand
            {
                OrderId = request.OrderId,
                UserId = GetUserId(),
                Stars = request.Stars,
                Comment = request.Comment
            };

            var success = await _mediator.Send(command);
            if (!success) return BadRequest(new { message = "Não foi possível adicionar a avaliação. Verifique se o pedido já foi avaliado ou se ainda não foi entregue." });

            return Ok();
        }

        public class AddReviewRequest
        {
            public Guid OrderId { get; set; }
            public int Stars { get; set; }
            public string? Comment { get; set; }
        }
    }
}
