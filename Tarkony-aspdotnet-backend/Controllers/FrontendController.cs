using Microsoft.AspNetCore.Mvc;
using Mongo.Services;

namespace Frontend.Controllers;

[Controller]
[Route("api/[controller]")]
public class FrontendController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public FrontendController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet("Categories")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var result = await _mongoDBService.GetCategoriesAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("ItemBase")]
    public async Task<IActionResult> GetItemBase()
    {
        try
        {
            var result = await _mongoDBService.GetItemBaseAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("ItemDetail/{id}")]
    public async Task<IActionResult> GetItemDetail([FromRoute] string id)
    {
        try
        {
            var result = await _mongoDBService.GetItemDetailAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
