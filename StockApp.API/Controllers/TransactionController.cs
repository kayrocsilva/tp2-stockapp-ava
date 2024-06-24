using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly IFraudDetectionService _fraudDetectionService;

        public TransactionController(IFraudDetectionService fraudDetectionService)
        {
            _fraudDetectionService = fraudDetectionService;
        }

        [HttpPost("process-transaction")]
        public async Task<IActionResult> ProcessTransaction(TransactionDTO transaction)
        {
            bool isFraudulent = await _fraudDetectionService.DetectFraudAsync(transaction);

            if (isFraudulent)
            {
                // Lógica para lidar com transações fraudulentas
                return BadRequest("Fraudulent transaction detected.");
            }
            else
            {
                // Lógica para processar transação normalmente
                return Ok("Transaction processed successfully.");
            }
        }
    }
}
