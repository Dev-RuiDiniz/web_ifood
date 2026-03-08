using System.Collections.Generic;

namespace Dtos.Analytics
{
    public class StoreStatsDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public List<TopProductDto> TopProducts { get; set; } = new();
        public List<HourlySalesDto> HourlySales { get; set; } = new();
    }

    public class TopProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class HourlySalesDto
    {
        public int Hour { get; set; }
        public int OrdersCount { get; set; }
    }
}
