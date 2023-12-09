using ECommerceMVC.Models;
using ECommerceMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Controllers;

public class UserController : Controller
{
    private readonly UserService _userService;
    
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsers();
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetUserById(id);
        return View(user);
    }

    [HttpPost]
    public Task Create(CreateUserRequest userData)
    {
        return _userService.CreateUserAsync(userData);
    }

    [HttpPut]
    public Task Update(UpdateUserRequest userData)
    {
        return _userService.UpdateUserAsync(userData);
    }

    [HttpDelete]
    public Task Delete(int id)
    {
        return _userService.DeleteUserAsync(id);
    }
}