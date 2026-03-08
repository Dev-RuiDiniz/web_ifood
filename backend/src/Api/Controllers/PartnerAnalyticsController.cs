using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Analytics;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize(Policy = "StoreOwnerOnly")]
    [ApiController]
    [Route("api/partner/analytics")]
    public class PartnerAnalyticsController : ControllerBase
    {
        private readonly IStoreAnalyticsService _analyticsService;

        public PartnerAnalyticsController(IStoreAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats([FromQuery] Guid restaurantId, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            // For production, verify if current user owns this restaurantId
            var stats = await _analyticsService.GetStoreStatsAsync(restaurantId, start, end);
            return Ok(stats);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportExcel([FromQuery] Guid restaurantId, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var content = await _analyticsService.ExportSalesToExcelAsync(restaurantId, start, end);
            return File(
                content, 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                $"vendas_{restaurantId}_{start:yyyyMMdd}.xlsx"
            );
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId!);
        }
    }
}
