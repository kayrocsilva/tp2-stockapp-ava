using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class TransactionDTO
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; } // Propriedade não anulável

        public DateTime TransactionDate { get; set; }

        public TransactionDTO()
        {
            Description = ""; // Inicializa com uma string vazia
        }
    }
}
