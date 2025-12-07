using Microsoft.AspNetCore.Mvc;
using Mongo.Services;

namespace Mongo.Controllers;

[Controller]
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
        try
        {
            var result = await _mongoDBService.GetSearcByNamehAsync(name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("ForceCategoriesDump")]
    public async Task<IActionResult> ForceCategoriesDump()
    {
        try
        {
            var result = await _mongoDBService.FetchCategoriesUploadAsync();
            return Created("api/items/ForceCategoriesDump", result);
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("ForceItemsDump")]
    public async Task<IActionResult> FetchItemsDump()
    {
        try
        {
            var result = await _mongoDBService.FetchItemsUploadAsync();
            return Created("api/items/ForceItemsDump", result);
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
