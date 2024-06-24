using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IPricingService _pricingService;
        private readonly IDiscountService _discountService;

        public ProductsController(IProductRepository productRepository, IPricingService pricingService)
        {
            _productRepository = productRepository;
            _pricingService = pricingService;
        }
        public ProductsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("calculate-discount")]
        public IActionResult CalculateDiscount(decimal price, decimal discountPercentage)
        {
            var discountedPrice = _discountService.ApplyDiscount(price, discountPercentage);
            return Ok(new { DiscountedPrice = discountedPrice });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("price/{productId}")]
        public async Task<ActionResult<decimal>> GetProductPrice(string productId)
        {
            var price = await _pricingService.GetProductPriceAsync(productId);
            return Ok(price);
        }

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Invalid image.");
            }

            // Verifica se o diretório existe, senão cria
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Caminho completo do arquivo
            var filePath = Path.Combine(directory, $"{id}_{DateTime.Now.Ticks}.jpg");

            // Salva a imagem no disco
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Retorna uma resposta de sucesso
            return Ok(new { imagePath = filePath });
        }
    }
}
