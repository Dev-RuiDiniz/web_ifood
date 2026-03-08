using MediatR;
using Domains.Entities;
using Repositories.Interfaces;
using Services.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Queries.SearchRestaurants
{
    public class SearchRestaurantsHandler : IRequestHandler<SearchRestaurantsQuery, IEnumerable<RestaurantResponseDto>>
    {
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        private readonly ICacheService _cacheService;

        public SearchRestaurantsHandler(IBaseRepository<Restaurant> restaurantRepository, ICacheService cacheService)
        {
            _restaurantRepository = restaurantRepository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<RestaurantResponseDto>> Handle(SearchRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"search:res:{request.Latitude}:{request.Longitude}:{request.RadiusKm}";
            var cached = await _cacheService.GetAsync<List<RestaurantResponseDto>>(cacheKey);
            if (cached != null) return cached;

            // Load restaurants with address (in a real scenario, this would be a spatial query in SQL)
            var restaurants = (await _restaurantRepository.GetAllAsync())
                .Where(r => r.Status == "Active");

            var result = new List<RestaurantResponseDto>();

            foreach (var r in restaurants)
            {
                if (r.Address == null || r.Address.Latitude == null || r.Address.Longitude == null) continue;

                var distance = CalculateDistance(
                    request.Latitude, request.Longitude,
                    r.Address.Latitude.Value, r.Address.Longitude.Value);

                if (distance <= request.RadiusKm)
                {
                    result.Add(new RestaurantResponseDto
                    {
                        Id = r.Id,
                        CommercialName = r.CommercialName,
                        Description = r.Description,
                        LogoUrl = r.LogoUrl,
                        DistanceKm = Math.Round(distance, 2),
                        IsOpen = r.IsOpen
                    });
                }
            }

            var finalResult = result.OrderBy(r => r.DistanceKm).ToList();
            await _cacheService.SetAsync(cacheKey, finalResult, TimeSpan.FromMinutes(5));

            return finalResult;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Earth radius in km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double deg) => deg * (Math.PI / 180);
    }
}
