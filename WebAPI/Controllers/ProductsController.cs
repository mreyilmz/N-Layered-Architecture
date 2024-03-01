using Business.Abstracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(
            IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("GetAllWithCategory")]
        public async Task<IActionResult> GetAllWithCategory()
        {
            return Ok(await _productService.GetAllWithCategoryAsync());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            return Ok(await _productService.AddAsync(product));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            return Ok(await _productService.UpdateAsync(product));
        }

        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
