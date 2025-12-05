/**
  [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(ILogger<ItemsController> logger)
        {
            _logger = logger;
        }

        public class IdDto
        {
            [Range(1, int.MaxValue, ErrorMessage = "Az ID nem lehet 0 vagy negatív.")]
            public int Id { get; set; }
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            _logger.LogInformation("GET /api/categories called");
            // Egyelőre csak struktúra, adat nélkül
            return Ok(new { message = "Categories endpoint működik" });
        }


        [HttpGet]
        public IActionResult GetItems()
        {
            _logger.LogInformation("GET /api/items called");
            // Egyelőre csak struktúra, adat nélkül
            return Ok(new { message = "Items endpoint működik" });
        }
        [HttpGet("{id}")]
        public IActionResult GetItemById([FromRoute] IdDto dto)
        {
            _logger.LogInformation("GET /api/items/{id} called", dto.Id);
           if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(new { message = $"Érvényes ID: {dto.Id}" });
        }

        //GET /items/by-name/{normalizedName}
        [HttpGet("by-name/{normalizedName}")]
        public IActionResult GetByName(string normalizedName)
        {
            _logger.LogInformation("GET /api/items/by-name/{Name} called", normalizedName);
            return Ok(new { normalizedName, message = "Item by name endpoint működik" });
        }
    }
*/