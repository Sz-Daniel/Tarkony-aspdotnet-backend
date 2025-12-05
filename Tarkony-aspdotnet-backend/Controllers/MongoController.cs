using System;
using Microsoft.AspNetCore.Mvc;
using Mongo.Services;


namespace Mongo.Controllers; 

[Controller]
[Route("mongodb/[controller]")]
public class MongoController: Controller {
    
    private readonly MongoDBService _mongoDBService;

    public MongoController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    [HttpGet("GetAllItems")]
    public async Task<List<ItemModel>> GetAllItems()
    {
        return await _mongoDBService.GetAllItemsAsync();
    }
}

[Controller]
[Route("mongodb/forced/[controller]")]
public class MongoForcedController: Controller {
    
    private readonly MongoDBService _mongoDBService;

    public MongoForcedController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
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
}


/*
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