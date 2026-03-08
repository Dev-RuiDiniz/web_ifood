using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Repositories.Interfaces;
using Domains.Entities;
using Services.Commands.Login;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação de usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOtpService _otpService;
        private readonly IBaseRepository<User> _userRepository;

        public AuthController(IMediator mediator, IOtpService otpService, IBaseRepository<User> userRepository)
        {
            _mediator = mediator;
            _otpService = otpService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command) // Modified Login method signature and logic
        {
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return Unauthorized(new { message = "Email ou senha inválidos." });
            }

            return Ok(result);
        }

        /// <summary>
        /// Envia um código OTP para o e-mail do usuário.
        /// </summary>
        [HttpPost("otp/send")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest request)
        {
            var users = await _userRepository.FindAsync(u => u.Email == request.Email);
            var user = users.FirstOrDefault();

            if (user == null) return NotFound(new { message = "Usuário não encontrado." });

            await _otpService.GenerateAndSendOtpAsync(user, request.Type);
            return Ok(new { message = "Código enviado com sucesso." });
        }

        /// <summary>
        /// Valida o código OTP.
        /// </summary>
        [HttpPost("otp/verify")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var users = await _userRepository.FindAsync(u => u.Email == request.Email);
            var user = users.FirstOrDefault();

            if (user == null) return NotFound(new { message = "Usuário não encontrado." });

            var isValid = await _otpService.ValidateOtpAsync(user.Id, request.Code, request.Type);

            if (!isValid) return BadRequest(new { message = "Código inválido ou expirado." });

            return Ok(new { message = "Código verificado com sucesso." });
        }

        public class SendOtpRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Type { get; set; } = "Login";
        }

        public class VerifyOtpRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
            public string Type { get; set; } = "Login";
        }
    }
}
