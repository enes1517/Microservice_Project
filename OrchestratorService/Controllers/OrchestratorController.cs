using Microsoft.AspNetCore.Mvc;
using OrchestratorService.Refit;
using ProductService.Dtos;
using ProductService.Dtos.AuthorDtos;

namespace OrchestratorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrchestratorController : ControllerBase
    {
        private readonly IProductServiceApi _productApi;

        public OrchestratorController(IProductServiceApi productApi)
        {
            _productApi = productApi;
        }

        #region Product Operations

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts([FromQuery] int n = 10)
        {
            var response = await _productApi.GetAllProductsAsync(n);
            return response.IsSuccessStatusCode ? Ok(response.Content) : StatusCode((int)response.Error.StatusCode);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var response = await _productApi.GetProductByIdAsync(id);
            return response.IsSuccessStatusCode ? Ok(response.Content) : StatusCode((int)response.Error.StatusCode);
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateTitleDto dto)
        {
            var response = await _productApi.CreateProductAsync(dto);
            return response.IsSuccessStatusCode ? Ok(response.Content) : StatusCode((int)response.Error.StatusCode);
        }

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateTitleDto dto)
        {
            var response = await _productApi.UpdateProductAsync(dto);
            return response.IsSuccessStatusCode ? Ok(response.Content) : StatusCode((int)response.Error.StatusCode);
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var response = await _productApi.DeleteProductAsync(id);
            return response.IsSuccessStatusCode ? Ok(response.Content) : StatusCode((int)response.Error.StatusCode);
        }

        #endregion

        #region Author Operations

        [HttpGet("authors")]
        public async Task<IActionResult> GetAuthors([FromQuery] int n = 5)
        {
            var response = await _productApi.GetAllAuthorsAsync(n);
            return response.IsSuccessStatusCode ? Ok(response.Content) : StatusCode((int)response.Error.StatusCode);
        }

        [HttpPost("authors")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDto dto)
        {
            var response = await _productApi.CreateAuthorAsync(dto);
            return response.IsSuccessStatusCode ? Ok(response.Content) : StatusCode((int)response.Error.StatusCode);
        }

        [HttpPut("authors")]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorDto dto)
        {
            var response = await _productApi.UpdateAuthorAsync(dto);
            return response.IsSuccessStatusCode ? Ok(response.Content) : StatusCode((int)response.Error.StatusCode);
        }

        [HttpDelete("authors/{id}")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            var response = await _productApi.DeleteAuthorAsync(id);
            return response.IsSuccessStatusCode ? Ok(response.Content) : StatusCode((int)response.Error.StatusCode);
        }

        #endregion
    }
}