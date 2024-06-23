﻿using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly IReviewRepository _reviewRepository;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products == null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }

            await _productService.Add(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest("Inconsistent Id");
            }
            if (product == null)
            {
                return BadRequest("Update Data Invalid");
            }

            await _productService.Update(product);

            return Ok(product);
        }
        [HttpPut("bulk-update")]
        public async Task<IActionResult> BulkUpdate([FromBody] List<Product> products)
        {
            await _productRepository.BulkUpdateAsync(products);
            return NoContent();
        }
        [HttpPost("compare")]
        public async Task<ActionResult<IEnumerable<Product>>> CompareProducts([FromBody] List<int> productIds)
        {
            var products = await _productRepository.GetByIdsAsync(productIds);
            return Ok(products);
        }
        [HttpPost("{productId}/review")]
        public async Task<IActionResult> AddReview(int productId, [FromBody] Review review)
        {
            review.ProductId = productId;
            review.Date = DateTime.Now;

            await _reviewRepository.AddAsync(review);
            return Ok();
        }
        private async Task NotifyExternalSystems(string eventData)
        {

        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromBody] WebhookDTO webhookDTO)
        {
            if (webhookDTO.EventType == "ProductCreated")
            {
                await NotifyExternalSystems(webhookDTO.EventData);
            }
            else if (webhookDTO.EventType == "CategoryUpdated")
            {
                await NotifyExternalSystems(webhookDTO.EventData);
            }

            return Ok();
        }
    }
}
