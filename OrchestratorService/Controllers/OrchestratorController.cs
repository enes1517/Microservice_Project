using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            _logger.LogInformation("Orchestrator:Ürün getirme isteği başladı.");

            var client = _httpClientFactory.CreateClient("ProductService");

            var response = await client.GetAsync("api/products");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Orchestrator: Ürünler başarıyla alındı.");
                return Ok(content);
            }
            _logger.LogError("Orchestrator: Ürünler alınamadı.");
            return StatusCode((int)response.StatusCode);


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var client = _httpClientFactory.CreateClient("ProductService");
            var response= await client.GetAsync($"api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Orchestrator: Ürünler başarıyla alındı.");
                return Ok(content);
            }
            _logger.LogError("Orchestrator: Ürünler alınamadı.");
            return StatusCode((int)response.StatusCode);

        }
    }
}
