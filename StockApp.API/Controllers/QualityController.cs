using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QualityController : ControllerBase
    {
        private readonly IQualityMonitoringService _qualityService;

        public QualityController(IQualityMonitoringService qualityService)
        {
            _qualityService = qualityService;
        }

        [HttpGet("{productId}/monitor")]
        public async Task<IActionResult> MonitorQuality(int productId)
        {
            var result = await _qualityService.MonitorQualityAsync(productId);
            return Ok(result);
        }
    }
}