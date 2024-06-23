using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class QualityMonitoringService : IQualityMonitoringService
    {
        public async Task<QualityReportDTO> MonitorQualityAsync(int productId)
        {
           return await Task.FromResult(new QualityReportDTO
            {
                ProductId = productId,
                QualityScore = 95,
                Comments = "Produto de alta qualidade"
            });
        }
    }
}