using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PubsContext _context;

        public ProductsController(PubsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task <ActionResult< IEnumerable<Title>>> GetProduct()
        {
            var product= await _context.Titles.Take(20).ToListAsync();
            return Ok(product);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Title>>> GetById(string id)
        {
            var title= await _context.Titles.FindAsync(id);
            if (title is null)
            {
                return NotFound();
            }
            return Ok(title);

        }
    }
}
