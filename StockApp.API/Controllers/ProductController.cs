using Microsoft.AspNetCore.Mvc;
using StockApp.Domain.Interfaces;
using System.Text;
using System.Threading.Tasks;
using StockApp.Domain.Interfaces;

namespace tp2_stockapp_ava.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportToCsv()
        {
            var products = await _productRepository.GetAllAsync(); // Certifique-se de que GetAllAsync está definido no IProductRepository e implementado no ProductRepository
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Description,Price,Stock");

            foreach (var product in products)
            {
                csv.AppendLine($"{product.Id},{product.Name},{product.Description},{product.Price},{product.StockQuantity}");
            }

            var fileName = $"products_{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}.csv";

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", fileName);
        }
    }
}
