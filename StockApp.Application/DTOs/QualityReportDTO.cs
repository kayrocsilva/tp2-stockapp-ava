using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class QualityReportDTO
    {
        public int ProductId { get; set; }
        public int QualityScore { get; set; }
        public string Comments { get; set; }
    }
}