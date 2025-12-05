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


    
/*

    [HttpGet("GetTest")]
    public async Task<List<ItemTestModel>> GetTest()
    {
        return await _mongoDBService.GetItemTestModelList();
    }

    [HttpGet("GetAllItems")]
    public async Task<List<ItemModel>> GetAllItems()
    {
        return await _mongoDBService.GetAllItemsAsync();
    }
[HttpGet("ForceItemsDataDump")]
    public async Task<IActionResult> ForceItemsDataDump()
    {
        try
        {
            var result = await _mongoDBService.FetchItemUploadAsync();
            return Created("api/items/ForceItemsDataDump", result);
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpDelete("ForceItemsDeleteDump")]
    public async Task<IActionResult> ForceItemsDeleteDump()
    {
        try
        {
            var result = await _mongoDBService.DeleteAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
            return StatusCode(500, new { error = ex.Message });
        }
    }
    [HttpGet]
    public async Task<List<Playlist>> Get() {
        return await _mongoDBService.GetAsync();
    }
        [HttpPost]
    public async Task<IActionResult> Post([FromBody] Playlist playlist) {
        await _mongoDBService.CreateAsync(playlist);
        return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) {    
        await _mongoDBService.AddToPlaylistAsync(id, movieId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }

*/