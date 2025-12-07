using System.Text.Json;
using GraphQL;
using Items.Adapter;
using Microsoft.AspNetCore.Mvc;

namespace Fetch.Controllers
{
    [ApiController]
    [Route("Fetch/[controller]")]
    public class FetchController : ControllerBase
    {
        private readonly GraphQLService _service;
        private readonly ILogger<FetchController> _logger;

        public FetchController(GraphQLService service, ILogger<FetchController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("Items")]
        public async Task<IActionResult> GetItemsData()
        {
            _logger.LogInformation("Items GraphQL fetch started");
            var items = await _service.FetchItemsAsync();
            return Ok(items);
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategoriesData()
        {
            _logger.LogInformation("Categories GraphQL fetch started");
            var items = await _service.FetchCategoriesAsync();
            return Ok(items);
        }
    }

    [ApiController]
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult HandleError()
        {
            return Problem("Valami hiba történt.");
        }
    }
}
