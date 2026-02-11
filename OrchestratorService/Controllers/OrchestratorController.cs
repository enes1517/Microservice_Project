using Microsoft.AspNetCore.Mvc;
using OrchestratorService.Refit;
using Shared.AuthorDtos;
using Shared.RequestParameters;
using Shared.TitleDtos;

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

        #region Product (Titles) Operations

        /// <summary>
        /// Tüm kitapları filtreli ve sayfalı şekilde getirir.
        /// </summary>
        [HttpGet("products")]
        public async Task<IActionResult> GetTitles([FromQuery] TitleRequestParameters p)
        {
            // İki ayrı metodun görevini tek bir metot üstlenir. 
            // Eğer p boş gelirse, CommonParameters içindeki varsayılanlar (1, 6) geçerli olur.
            var response = await _productApi.GetAllTitlesAsync(p);

            return response.IsSuccessStatusCode
                ? Ok(response.Content)
                : StatusCode((int)response.Error.StatusCode, response.Error.Content);
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

        /// <summary>
        /// Tüm yazarları filtreli ve sayfalı şekilde getirir.
        /// </summary>
        [HttpGet("authors")]
        public async Task<IActionResult> GetAuthors([FromQuery] AuthorRequestParameters p)
        {
            var response = await _productApi.GetAllAuthorsAsync(p);

            return response.IsSuccessStatusCode
                ? Ok(response.Content)
                : StatusCode((int)response.Error.StatusCode, response.Error.Content);
        }

        [HttpGet("authors/{id}")]
        public async Task<IActionResult> GetAuthor(string id)
        {
            var response = await _productApi.GetAuthorByIdAsync(id);
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