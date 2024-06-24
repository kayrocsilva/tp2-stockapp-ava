using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class ContractDTO
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Terms { get; set; }
    }
}