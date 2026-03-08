namespace Dtos.Analytics
{
    public class AdminDashboardDto
    {
        public decimal TotalGMV { get; set; } // Total volume of sales
        public int TotalOrders { get; set; }
        public int ActiveUsers { get; set; }
        public int RegisteredRestaurants { get; set; }
        public decimal AverageTicket { get; set; }
        public decimal TotalCommissionEarned { get; set; }
    }
}
