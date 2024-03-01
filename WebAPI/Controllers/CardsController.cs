using Business.Abstracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private readonly ICardService _cardService;

        public CardsController(
            ICardService cardService
            )
        {
            _cardService = cardService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cardService.GetAllAsync());
        }

        [HttpGet("GetAllWithUser")]
        public async Task<IActionResult> GetAllWithUser()
        {
            return Ok(await _cardService.GetAllWithUserAsync());
        }

        [HttpGet("GetAllWithCardType")]
        public async Task<IActionResult> GetAllWithCardType()
        {
            return Ok(await _cardService.GetAllWithCardTypeAsync());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _cardService.GetByIdAsync(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Card card)
        {
            return Ok(await _cardService.AddAsync(card));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Card card)
        {
            return Ok(await _cardService.UpdateAsync(card));
        }

        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cardService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}

