using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.RealTime
{
    public interface ICourierGeoService
    {
        Task UpdateLocationAsync(Guid courierId, double latitude, double longitude);
        Task<IEnumerable<Guid>> GetNearbyCouriersAsync(double latitude, double longitude, double radiusKm);
        Task RemoveCourierAsync(Guid courierId);
    }

    public class CourierGeoService : ICourierGeoService
    {
        private readonly IConnectionMultiplexer _redis;
        private const string GeoKey = "courier_locations";

        public CourierGeoService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task UpdateLocationAsync(Guid courierId, double latitude, double longitude)
        {
            var db = _redis.GetDatabase();
            await db.GeoAddAsync(GeoKey, longitude, latitude, courierId.ToString());
        }

        public async Task<IEnumerable<Guid>> GetNearbyCouriersAsync(double latitude, double longitude, double radiusKm)
        {
            var db = _redis.GetDatabase();
            var results = await db.GeoRadiusAsync(GeoKey, longitude, latitude, radiusKm, GeoUnit.Kilometers);
            
            return results.Select(r => Guid.Parse(r.Member.ToString()));
        }

        public async Task RemoveCourierAsync(Guid courierId)
        {
            var db = _redis.GetDatabase();
            await db.GeoRemoveAsync(GeoKey, courierId.ToString());
        }
    }
}
