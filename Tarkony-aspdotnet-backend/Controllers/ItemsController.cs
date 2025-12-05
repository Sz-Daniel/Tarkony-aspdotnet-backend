using Microsoft.AspNetCore.Mvc;
using GraphQL;
namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly GraphQLService _service;
        private readonly ILogger<ItemsController> _logger;
        public ItemsController(GraphQLService service, ILogger<ItemsController> logger)
        {
            _service = service;
            _logger = logger;
        }
        
        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategoriesData()
        {
            _logger.LogInformation("Categories GraphQL fetch started");
            var items = await _service.FetchCategoriesAsync();
        
            return Ok(items); 
        }

        [HttpGet("ItemBase")]
        public async Task<IActionResult> GetItemBaseData()
        {
            _logger.LogInformation("ItemBase GraphQL fetch started");
            var items = await _service.FetchItemBaseAsync();
        
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


