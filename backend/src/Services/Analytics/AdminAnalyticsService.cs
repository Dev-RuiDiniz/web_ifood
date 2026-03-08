using Dapper;
using Dtos.Analytics;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Threading.Tasks;

namespace Services.Analytics
{
    public interface IAdminAnalyticsService
    {
        Task<AdminDashboardDto> GetMainDashboardStatsAsync();
    }

    public class AdminAnalyticsService : IAdminAnalyticsService
    {
        private readonly string _connectionString;

        public AdminAnalyticsService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<AdminDashboardDto> GetMainDashboardStatsAsync()
        {
            using IDbConnection db = new NpgsqlConnection(_connectionString);

            const string sql = @"
                SELECT 
                    COALESCE(SUM(""TotalAmount""), 0) AS TotalGMV,
                    COUNT(*) AS TotalOrders,
                    (SELECT COUNT(*) FROM ""Users"") AS ActiveUsers,
                    (SELECT COUNT(*) FROM ""Restaurants"") AS RegisteredRestaurants,
                    COALESCE(AVG(""TotalAmount""), 0) AS AverageTicket,
                    COALESCE(SUM(""DiscountAmount""), 0) AS TotalDiscountsUsed
                FROM ""Orders""
                WHERE ""Status"" NOT IN ('Cancelled', 'Created');
            ";

            var stats = await db.QuerySingleAsync<dynamic>(sql);

            return new AdminDashboardDto
            {
                TotalGMV = (decimal)stats.totalgmv,
                TotalOrders = (int)stats.totalorders,
                ActiveUsers = (int)stats.activeusers,
                RegisteredRestaurants = (int)stats.registeredrestaurants,
                AverageTicket = (decimal)stats.averageticket,
                TotalCommissionEarned = (decimal)stats.totalgmv * 0.15m // Approximate for MVP
            };
        }
    }
}
