using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class PaymentRequestDTO
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvc { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}