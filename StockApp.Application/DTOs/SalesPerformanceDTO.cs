using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class SalesPerformanceDTO
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
        // Outros campos relevantes para o desempenho de vendas

        // Construtor vazio necessário para desserialização JSON
        public SalesPerformanceDTO()
        {
        }
    }
}
