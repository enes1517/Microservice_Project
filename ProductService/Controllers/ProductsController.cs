using Microsoft.AspNetCore.Mvc;
using ProductService.Services.Contracts;
using Shared.Models;
using Shared.RequestParameters;
using Shared.TitleDtos;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ITitleServices _manager;

        public ProductsController(ITitleServices manager)
        {
            _manager = manager;
        }

 

        [HttpGet]
        public async Task<List<Title>> GetAllTitlesAsync([FromQuery]TitleRequestParameters p)
        {
            return  await _manager.GetAllTitlesAsync(p);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var result = await _manager.GetByIdAsync(id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateTitleDto dto)
        {
            var result = await _manager.CreateTitle(dto);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateTitleDto dto)
        {
            var result = await _manager.UpdateAsync(dto);
            if (result is false)
                return NotFound();

            return Ok(new { Message = "Güncellendi" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _manager.DeleteAsync(id);
            if (result is false)
                return NotFound();

            return Ok(new { Message = "Silindi" });
        }
    }
}
