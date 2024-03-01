using Business.Abstracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(
            IOrderService orderService
            )
        {
            _orderService = orderService;
        }

        [HttpGet("GetAll")]
        public async Task< IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAllAsync());
        }

        [HttpGet("GetById/{id}")]
        public async Task< IActionResult> Get(Guid id)
        {
            return Ok(await _orderService.GetByIdAsync(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddOrderDto addOrderDto)
        {
            return Ok(await _orderService.AddAsync(addOrderDto));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Order order)
        {
            return Ok(await _orderService.UpdateAsync(order));
        }

        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
