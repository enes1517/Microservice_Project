using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Services.Contracts;
using Shared.AuthorDtos;
using Shared.Models;
using Shared.RequestParameters;

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

        [HttpGet]
        public async Task<IActionResult> GetAllAuthorsAsync([FromQuery]AuthorRequestParameters p)
        {
            var result = await _manager.GetAllAuthorsAsync(p);
            
            return Ok(new 
            {
                items = result.Authors,
                totalCount = result.TotalCount
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(string id)
        {
            var author = await _manager.GetAuthorByIdAsync(id);
            if (author is null) return NotFound();
            
            // Map to UpdateAuthorDto or AuthorViewDto as expected by frontend
            // Frontend expects UpdateAuthorDto for editing
            var authorDto = new UpdateAuthorDto
            {
                Id = author.AuId,
                AuFname = author.AuFname,
                AuLname = author.AuLname,
                Phone = author.Phone,
                Address = author.Address,
                City = author.City,
                Contract = author.Contract
            };
            
            return Ok(authorDto);
        }

     
        [HttpPost]
        public async Task<IActionResult> CreateAuthors([FromBody]AuthorCreateDto dto)
        {
         
                var result = await _manager.AuthorCreateAsync(dto);

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
        public async Task<IActionResult> DeleteAuthors(string id)
        {
            var result= await _manager.AuthorDeleteAsync(id);
            if (result is false) return NotFound();

            return Ok(new { Message = "Silindi" });

        }






    }

}
