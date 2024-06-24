using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class PaymentResultDTO
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}