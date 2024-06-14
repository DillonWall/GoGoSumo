using GoGoSumo.Server.Models.ApiModels;
using GoGoSumo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoGoSumo.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserModel model)
    {
        await _userService.Create(model);
        return Ok(new { message = "User created" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserModel model)
    {
        await _userService.Update(id, model);
        return Ok(new { message = "User updated" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}
