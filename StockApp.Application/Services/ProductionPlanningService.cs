using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class ProductionPlanningService : IProductionPlanningService
    {
        private readonly List<ProductionPlanDTO> _productionPlans;
        private int _nextPlanId;

        public ProductionPlanningService()
        {
            _productionPlans = new List<ProductionPlanDTO>();
            _nextPlanId = 1; // Simulação de um identificador sequencial
        }

        public async Task<int> PlanProductionAsync(ProductionPlanDTO productionPlan)
        {
            productionPlan.PlanId = _nextPlanId++;
            _productionPlans.Add(productionPlan);
            await Task.Delay(100); // Simulação de operação assíncrona
            return productionPlan.PlanId;
        }

        public async Task<ProductionPlanDTO> GetProductionPlanAsync(int planId)
        {
            await Task.Delay(100); // Simulação de operação assíncrona
            return _productionPlans.Find(plan => plan.PlanId == planId);
        }

        public async Task UpdateProductionPlanAsync(int planId, ProductionPlanDTO productionPlan)
        {
            var index = _productionPlans.FindIndex(plan => plan.PlanId == planId);
            if (index != -1)
            {
                productionPlan.PlanId = planId;
                _productionPlans[index] = productionPlan;
            }
            await Task.Delay(100); // Simulação de operação assíncrona
        }

        public async Task DeleteProductionPlanAsync(int planId)
        {
            _productionPlans.RemoveAll(plan => plan.PlanId == planId);
            await Task.Delay(100); // Simulação de operação assíncrona
        }
    }
}
