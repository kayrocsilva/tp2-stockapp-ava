using StockApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IProductionPlanningService
    {
        Task<int> PlanProductionAsync(ProductionPlanDTO productionPlan);
        Task<ProductionPlanDTO> GetProductionPlanAsync(int planId);
        Task UpdateProductionPlanAsync(int planId, ProductionPlanDTO productionPlan);
        Task DeleteProductionPlanAsync(int planId);
    }
}
