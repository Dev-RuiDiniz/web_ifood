using MediatR;
using System.Collections.Generic;

namespace Services.Queries.SearchRestaurants
{
    public class SearchRestaurantsQuery : IRequest<IEnumerable<RestaurantResponseDto>>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double RadiusKm { get; set; } = 5.0; // Default 5km
        public string? Category { get; set; }
    }

    public class RestaurantResponseDto
    {
        public Guid Id { get; set; }
        public string CommercialName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public double DistanceKm { get; set; }
        public bool IsOpen { get; set; }
    }
}
