using ClosedXML.Excel;
using Dapper;
using Dtos.Analytics;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Analytics
{
    public interface IStoreAnalyticsService
    {
        Task<StoreStatsDto> GetStoreStatsAsync(Guid restaurantId, DateTime start, DateTime end);
        Task<byte[]> ExportSalesToExcelAsync(Guid restaurantId, DateTime start, DateTime end);
    }

    public class StoreAnalyticsService : IStoreAnalyticsService
    {
        private readonly string _connectionString;

        public StoreAnalyticsService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<StoreStatsDto> GetStoreStatsAsync(Guid restaurantId, DateTime start, DateTime end)
        {
            using IDbConnection db = new NpgsqlConnection(_connectionString);

            const string statsSql = @"
                SELECT 
                    COALESCE(SUM(""TotalAmount""), 0) AS TotalRevenue,
                    COUNT(*) AS TotalOrders
                FROM ""Orders""
                WHERE ""RestaurantId"" = @restaurantId 
                  AND ""CreatedAt"" BETWEEN @start AND @end
                  AND ""Status"" NOT IN ('Cancelled', 'Created');
            ";

            const string topProductsSql = @"
                SELECT 
                    ""ProductName"",
                    SUM(""Quantity"") AS QuantitySold,
                    SUM(""Quantity"" * ""UnitPrice"") AS TotalRevenue
                FROM ""OrderItems""
                JOIN ""Orders"" ON ""OrderItems"".""OrderId"" = ""Orders"".""Id""
                WHERE ""Orders"".""RestaurantId"" = @restaurantId
                  AND ""Orders"".""CreatedAt"" BETWEEN @start AND @end
                  AND ""Orders"".""Status"" NOT IN ('Cancelled', 'Created')
                GROUP BY ""ProductName""
                ORDER BY QuantitySold DESC
                LIMIT 5;
            ";

            var stats = await db.QuerySingleAsync<dynamic>(statsSql, new { restaurantId, start, end });
            var topProducts = await db.QueryAsync<TopProductDto>(topProductsSql, new { restaurantId, start, end });

            return new StoreStatsDto
            {
                TotalRevenue = (decimal)stats.totalrevenue,
                TotalOrders = (int)stats.totalorders,
                TopProducts = topProducts.ToList()
            };
        }

        public async Task<byte[]> ExportSalesToExcelAsync(Guid restaurantId, DateTime start, DateTime end)
        {
            using IDbConnection db = new NpgsqlConnection(_connectionString);
            
            const string sql = @"
                SELECT 
                    ""OrderNumber"", ""CreatedAt"", ""TotalAmount"", ""Status"", ""DeliveryFee""
                FROM ""Orders""
                WHERE ""RestaurantId"" = @restaurantId 
                  AND ""CreatedAt"" BETWEEN @start AND @end
                ORDER BY ""CreatedAt"" DESC;
            ";

            var sales = await db.QueryAsync<dynamic>(sql, new { restaurantId, start, end });

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Vendas");

            worksheet.Cell(1, 1).Value = "Pedido";
            worksheet.Cell(1, 2).Value = "Data";
            worksheet.Cell(1, 3).Value = "Valor Total";
            worksheet.Cell(1, 4).Value = "Status";
            worksheet.Cell(1, 5).Value = "Taxa Entrega";

            int row = 2;
            foreach (var s in sales)
            {
                worksheet.Cell(row, 1).Value = s.OrderNumber;
                worksheet.Cell(row, 2).Value = s.CreatedAt;
                worksheet.Cell(row, 3).Value = s.TotalAmount;
                worksheet.Cell(row, 4).Value = s.Status;
                worksheet.Cell(row, 5).Value = s.DeliveryFee;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
