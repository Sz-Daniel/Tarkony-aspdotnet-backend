using System;
using Categories;
using Item;
using ItemBase;
using Microsoft.AspNetCore.Mvc;
using Mongo.Services;


namespace Mongo.Controllers; 

[Controller]
[Route("mongodb/[controller]")]
public class MongoController: Controller {

    //DI
    private readonly MongoDBService _mongoDBService;
    public MongoController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    // Get section

/**
    [HttpGet("GetCategories")]
    public async Task<List<CategoryModel>> GetCategories()
    {
        return await _mongoDBService.GetCategoriesAsync();
    }

    [HttpGet("GetItemBase")]
    public async Task<List<ItemBaseModel>> GetItemBase()
    {
        return await _mongoDBService.GetItemBaseAsync();
    }
*/


}

// Manually "Forced" database function calls

[Controller]
[Route("mongodb/forced/[controller]")]
public class MongoForcedController: Controller {
    
    private readonly MongoDBService _mongoDBService;
    public MongoForcedController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
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

    [HttpGet("ForceItemBaseDump")]
    public async Task<IActionResult> FetchItemBaseDump()
    {
        try
        {
            var result = await _mongoDBService.FetchItemBaseUploadAsync();
            return Created("api/items/ForceItemBaseDump", result);
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
            return StatusCode(500, new { error = ex.Message });
        }
    }


}

