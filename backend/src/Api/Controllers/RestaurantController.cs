using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Queries.SearchRestaurants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Busca restaurantes próximos a uma localização específica.
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] double lat, [FromQuery] double lng, [FromQuery] double radius = 5.0)
        {
            var query = new SearchRestaurantsQuery
            {
                Latitude = lat,
                Longitude = lng,
                RadiusKm = radius
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
