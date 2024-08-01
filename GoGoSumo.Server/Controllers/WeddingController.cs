using GoGoSumo.DTOs.Models.Wedding;
using GoGoSumo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoGoSumo.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WeddingController : ControllerBase
{
    private IWeddingService _weddingService;

    public WeddingController(IWeddingService weddingService)
    {
        _weddingService = weddingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var weddings = await _weddingService.GetAll();
        return Ok(weddings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var weddings = await _weddingService.GetById(id);
        return Ok(weddings);
    }

    [HttpPost]
    public async Task<IActionResult> Create(WeddingCreateModel model)
    {
        await _weddingService.Create(model);
        return Ok(new { message = "Wedding created" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(WeddingUpdateModel model)
    {
        await _weddingService.Update(model);
        return Ok(new { message = "Wedding updated" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _weddingService.Delete(id);
        return Ok(new { message = "Wedding deleted" });
    }
}
