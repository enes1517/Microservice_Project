using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Dtos;
using ProductService.Models;
using ProductService.Services.Contracts;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PubsContext _context;
        private readonly ITitleServices _service;
        public ProductsController(PubsContext context, ITitleServices service)
        {
            _context = context;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] int n = 10)
        {
            // n deðerini query'den alýyoruz
            var product = await _service.GetAllAsync(n);
            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var title = await _context.Titles.FindAsync(id);
            if (title is null)
            {
                return NotFound();
            }
            return Ok(title);

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTitleDto dto)
        {
            var result = await _service.CreateTitle(dto);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTitleDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (!result)
                return NotFound();
            return Ok(new { Message = "Güncellendi" });

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
           var result= await _service.DeleteAsync(id);
           if(!result)
                return NotFound();
            return Ok(new { Message = "Silindi" });

        }



    }
}
