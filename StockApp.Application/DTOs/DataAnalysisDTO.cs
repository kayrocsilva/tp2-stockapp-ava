using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class DataAnalysisDTO
    {
        public string AnalysisId { get; set; }
        public DateTime AnalysisDate { get; set; }
        public string Result { get; set; }
        // Outros campos relevantes para o resultado da análise

        // Construtor vazio necessário para desserialização JSON
        public DataAnalysisDTO()
        {
        }
    }
}
