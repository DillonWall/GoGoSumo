using GoGoSumo.DTOs.Models.User;
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

    [HttpGet("{clerk_id}")]
    public async Task<IActionResult> GetById(string clerk_id)
    {
        var user = await _userService.GetById(clerk_id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateModel model)
    {
        await _userService.Create(model);
        return Ok(new { message = "User created" });
    }

    [HttpPut("{clerk_id}")]
    public async Task<IActionResult> Update(string clerk_id, UserUpdateModel model)
    {
        await _userService.Update(clerk_id, model);
        return Ok(new { message = "User updated" });
    }

    [HttpDelete("{clerk_id}")]
    public async Task<IActionResult> Delete(string clerk_id)
    {
        await _userService.Delete(clerk_id);
        return Ok(new { message = "User deleted" });
    }
}
