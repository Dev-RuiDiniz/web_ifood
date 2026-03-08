using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.AddAddress;
using Services.Commands.UpdateProfile;
using Services.Queries.GetUserDetails;
using Repositories.Interfaces;
using Data.Interfaces;
using Domains.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Services.Privacy;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPrivacyService _privacyService;
        private readonly IBaseRepository<TermConsent> _consentRepository;
        private readonly IUnitOfWork _uow;

        public ProfileController(
            IMediator mediator, 
            IPrivacyService privacyService, 
            IBaseRepository<TermConsent> consentRepository, 
            IUnitOfWork uow)
        {
            _mediator = mediator;
            _privacyService = privacyService;
            _consentRepository = consentRepository;
            _uow = uow;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId!);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = GetUserId();
            var query = new GetUserDetailsQuery(userId);
            var result = await _mediator.Send(query);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("addresses")]
        public async Task<IActionResult> ListAddresses()
        {
            var query = new global::Services.Queries.ListUserAddresses.ListUserAddressesQuery(GetUserId());
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var command = new UpdateProfileCommand
            {
                UserId = GetUserId(),
                Name = request.Name,
                Phone = request.Phone
            };

            var success = await _mediator.Send(command);
            if (!success) return BadRequest();

            return NoContent();
        }

        [HttpDelete("me")]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = GetUserId();
            var success = await _privacyService.AnonymizeUserAsync(userId);
            
            if (!success) return BadRequest();

            return NoContent();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportData()
        {
            var userId = GetUserId();
            var data = await _privacyService.ExportUserDataAsync(userId);
            
            if (string.IsNullOrEmpty(data)) return NotFound();

            return Content(data, "application/json");
        }

        [HttpPost("consent")]
        public async Task<IActionResult> AddConsent([FromBody] ConsentRequest request)
        {
            var consent = new TermConsent
            {
                UserId = GetUserId(),
                TermVersion = request.TermVersion,
                IsAccepted = request.IsAccepted,
                ConsentedAt = DateTime.UtcNow
            };

            await _consentRepository.AddAsync(consent);
            await _uow.CommitAsync();

            return Ok();
        }

        [HttpPost("addresses")]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressRequest request)
        {
            var command = new AddAddressCommand
            {
                UserId = GetUserId(),
                Street = request.Street,
                Number = request.Number,
                Neighborhood = request.Neighborhood,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                IsMain = request.IsMain
            };

            try
            {
                var addressId = await _mediator.Send(command);
                return CreatedAtAction(nameof(ListAddresses), new { id = addressId }, addressId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("addresses/{id}/main")]
        public async Task<IActionResult> SetMainAddress(Guid id)
        {
            var command = new global::Services.Commands.SetMainAddress.SetMainAddressCommand(GetUserId(), id);
            var success = await _mediator.Send(command);
            
            if (!success) return NotFound();

            return NoContent();
        }

        public class UpdateProfileRequest
        {
            public string Name { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
        }

        public class AddAddressRequest
        {
            public string Street { get; set; } = string.Empty;
            public string Number { get; set; } = string.Empty;
            public string Neighborhood { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
            public string ZipCode { get; set; } = string.Empty;
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
            public bool IsMain { get; set; }
        }

        public class ConsentRequest
        {
            public string TermVersion { get; set; } = string.Empty;
            public bool IsAccepted { get; set; }
        }
    }
}
