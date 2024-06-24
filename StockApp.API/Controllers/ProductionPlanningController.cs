using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class ProductionPlanningController : ControllerBase
        {
            private readonly IProductionPlanningService _productionPlanningService;

            public ProductionPlanningController(IProductionPlanningService productionPlanningService)
            {
                _productionPlanningService = productionPlanningService;
            }

            [HttpPost("plan")]
            public async Task<IActionResult> PlanProduction([FromBody] ProductionPlanDTO productionPlan)
            {
                var planId = await _productionPlanningService.PlanProductionAsync(productionPlan);
                return Ok(planId);
            }

            [HttpGet("{planId}")]
            public async Task<IActionResult> GetProductionPlan(int planId)
            {
                var plan = await _productionPlanningService.GetProductionPlanAsync(planId);
                if (plan == null)
                    return NotFound();
                return Ok(plan);
            }

            [HttpPut("{planId}")]
            public async Task<IActionResult> UpdateProductionPlan(int planId, [FromBody] ProductionPlanDTO productionPlan)
            {
                await _productionPlanningService.UpdateProductionPlanAsync(planId, productionPlan);
                return Ok();
            }

            [HttpDelete("{planId}")]
            public async Task<IActionResult> DeleteProductionPlan(int planId)
            {
                await _productionPlanningService.DeleteProductionPlanAsync(planId);
                return Ok();
            }
        }
    }


