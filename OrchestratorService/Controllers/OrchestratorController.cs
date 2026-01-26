using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using System.Net.Http.Json; // JsonContent desteği için
using System.Threading.Tasks;

namespace OrchestratorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrchestratorController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrchestratorController> _logger;

        public OrchestratorController(IHttpClientFactory httpClientFactory, ILogger<OrchestratorController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // GET: api/orchestrator
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int n)
        {
            _logger.LogInformation($"Orchestrator: {n} adet ürün getirme isteği başladı.");

            var client = _httpClientFactory.CreateClient("ProductService");

            // URL'ye query string ekliyoruz: api/products?count=5
            var response = await client.GetAsync($"api/products?n={n}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return StatusCode((int)response.StatusCode);
        }

        // GET: api/orchestrator/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient("ProductService");
            var response = await client.GetAsync($"api/products/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return StatusCode((int)response.StatusCode);
        }

        // POST: api/orchestrator
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateTitleDto productDto)
        {
            _logger.LogInformation("Orchestrator: Yeni ürün oluşturma isteği.");
            var client = _httpClientFactory.CreateClient("ProductService");
            var response = await client.PostAsJsonAsync("api/products", productDto);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return StatusCode((int)response.StatusCode);
        }

        // PUT: api/orchestrator
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateTitleDto updateProductDto)
        {
            _logger.LogInformation("Orchestrator: Ürün güncelleme isteği.");
            var client = _httpClientFactory.CreateClient("ProductService");
            var response = await client.PutAsJsonAsync("api/products", updateProductDto);

            if (response.IsSuccessStatusCode)
                return Ok(new { Message = "Güncelleme başarılı" });

            return StatusCode((int)response.StatusCode);
        }

        // DELETE: api/orchestrator/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            _logger.LogInformation($"Orchestrator: {id} id'li ürün silme isteği.");
            var client = _httpClientFactory.CreateClient("ProductService");
            var response = await client.DeleteAsync($"api/products/{id}");

            if (response.IsSuccessStatusCode)
                return Ok(new { Message = "Silme işlemi başarılı" });

            return StatusCode((int)response.StatusCode);
        }
    }
}