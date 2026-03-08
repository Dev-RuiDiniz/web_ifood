using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.CreateUser;
using Services.Queries.GetUserDetails;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public class CreateUserRequest
        {
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string Document { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var command = new CreateUserCommand
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Document = request.Document,
                Phone = request.Phone
            };

            var userId = await _mediator.Send(command);

            return CreatedAtAction(nameof(Create), new { id = userId }, userId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetUserDetailsQuery(id);
            var result = await _mediator.Send(query);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("error-test")]
        public IActionResult ErrorTest()
        {
            throw new Exception("Teste de exceção global!");
        }
    }
}
