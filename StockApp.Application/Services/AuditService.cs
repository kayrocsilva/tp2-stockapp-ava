using Microsoft.Extensions.Logging;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class AuditService : IAuditService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<AuditService> _logger;

        public AuditService(IProductRepository productRepository, ILogger<AuditService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task AuditStockChange(int productId, int oldStock, int newStock)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            _logger.LogInformation($"Audit stock change - Product: {product.Name}, Old Stock: {oldStock}, New Stock: {newStock}");
        }
    }
}