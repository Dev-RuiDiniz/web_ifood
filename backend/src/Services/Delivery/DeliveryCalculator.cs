using System;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories.Interfaces;

namespace Services.Delivery
{
    public interface IDeliveryCalculator
    {
        Task<decimal> CalculateFeeAsync(Guid restaurantId, Guid addressId, decimal orderTotal);
    }

    public class DeliveryCalculator : IDeliveryCalculator
    {
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        private readonly IBaseRepository<Address> _addressRepository;
        private readonly IBaseRepository<DeliveryFeeConfig> _feeConfigRepository;

        public DeliveryCalculator(
            IBaseRepository<Restaurant> restaurantRepository, 
            IBaseRepository<Address> addressRepository, 
            IBaseRepository<DeliveryFeeConfig> feeConfigRepository)
        {
            _restaurantRepository = restaurantRepository;
            _addressRepository = addressRepository;
            _feeConfigRepository = feeConfigRepository;
        }

        public async Task<decimal> CalculateFeeAsync(Guid restaurantId, Guid addressId, decimal orderTotal)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            var customerAddress = await _addressRepository.GetByIdAsync(addressId);

            if (restaurant == null || customerAddress == null) return 0;
            if (restaurant.Address == null) return 0;

            var distance = CalculateDistance(
                restaurant.Address.Latitude ?? 0, restaurant.Address.Longitude ?? 0,
                customerAddress.Latitude ?? 0, customerAddress.Longitude ?? 0
            );

            var configs = await _feeConfigRepository.FindAsync(c => c.RestaurantId == restaurantId && c.IsActive);
            var config = configs
                .Where(c => distance >= c.MinDistanceKm && distance <= c.MaxDistanceKm)
                .OrderBy(c => c.MinDistanceKm)
                .FirstOrDefault();

            if (config == null) return 0;

            if (config.FreeShippingThreshold.HasValue && orderTotal >= config.FreeShippingThreshold.Value)
            {
                return 0;
            }

            return config.Fee;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371;
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
