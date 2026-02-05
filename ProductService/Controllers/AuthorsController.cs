using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Services.Contracts;
using Shared.AuthorDtos;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorServices _manager;

        public AuthorsController(IAuthorServices manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAuthors([FromQuery] int n=5)
        {
          var result= await _manager.GetListAsync(n);
        if (result is null)
                return NotFound();

        return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthors([FromBody]AuthorCreateDto dto)
        {
            var result= await _manager.AuthorCreateAsync(dto);
            
            if (result is null) return NotFound();
            
            return Ok(result);


        }
        [HttpPut]
        public async Task<IActionResult> UpdateAuthors([FromBody] UpdateAuthorDto dto)
        {
            var result= await _manager.AuthorUpdateAsync(dto);
            
            if(result is false)  return NotFound();

            return Ok(new { Message = "Güncellendi" });

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthors([FromQuery] string id)
        {
            var result= await _manager.AuthorDeleteAsync(id);
            if (result is false) return NotFound();

            return Ok(new { Message = "Silindi" });

        }






    }

}
