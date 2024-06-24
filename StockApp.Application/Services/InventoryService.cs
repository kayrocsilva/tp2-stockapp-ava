using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;

        public InventoryService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ReplenishStockAsync()
        {
            var lowStockProducts = await _productRepository.GetLowStockAsync(10); // threshold de exemplo

            foreach (var product in lowStockProducts)
            {
                // Verifica se o objeto é realmente do tipo Product
                if (product is Product validProduct)
                {
                    // Quantidade de reposição
                    int replenishAmount = 50; // quantidade de reposição de exemplo

                    // Incrementa o estoque do produto
                    validProduct.Stock += replenishAmount;

                    // Atualiza o produto no repositório
                    await _productRepository.UpdateAsync(validProduct);
                }
                else
                {
                    // Log ou tratamento de erro para objetos que não são do tipo Product
                    Console.WriteLine($"Objeto não é do tipo Product: {product.GetType().FullName}");
                }
            }
        }
    }
}
