using Microsoft.AspNetCore.Mvc;
using Mongo.Services;

namespace Mongo.Controllers;

[ApiController]
[Route("Mongo/[controller]")]
public class MongoController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public MongoController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet("SearchItemByName")]
    public async Task<IActionResult> SearchItemByName([FromRoute] string name)
    {
        var result = await _mongoDBService.GetSearcByNameAsync(name);
        return Ok(result);
    }

    [HttpGet("ForceCategoriesDump")]
    public async Task<IActionResult> ForceCategoriesDump()
    {
        var result = await _mongoDBService.FetchCategoriesUploadAsync();
        return Created("api/items/ForceCategoriesDump", result);
    }

    [HttpGet("ForceItemsDump")]
    public async Task<IActionResult> FetchItemsDump()
    {
        var result = await _mongoDBService.FetchItemsUploadAsync();
        return Created("api/items/ForceItemsDump", result);
    }
}
