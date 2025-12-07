using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Mongo.Services;

namespace Frontend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FrontendController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public FrontendController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [EnableRateLimiting("frontend")]
    [HttpGet("Categories")]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _mongoDBService.GetCategoriesAsync();
        return Ok(result);
    }

    [EnableRateLimiting("frontend")]
    [HttpGet("ItemBase")]
    public async Task<IActionResult> GetItemBase()
    {
        var result = await _mongoDBService.GetItemBaseAsync();
        return Ok(result);
    }

    [EnableRateLimiting("frontend")]
    [HttpGet("ItemDetail/{id}")]
    public async Task<IActionResult> GetItemDetail([FromRoute] string id)
    {
        var result = await _mongoDBService.GetItemDetailAsync(id);
        return Ok(result);
    }
}
