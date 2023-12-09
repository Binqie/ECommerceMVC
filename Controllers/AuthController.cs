using ECommerceMVC.Models;
using ECommerceMVC.Models.Auth;
using ECommerceMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMVC.Controllers;

public class AuthController : Controller
{
    private readonly UserService _userService;
    
    public AuthController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }
    
    [HttpPost]
    public Task SignUp(CreateUserRequest userData)
    {
        return _userService.CreateUserAsync(userData);
    }
    
    [HttpPost]
    public IActionResult SignIn(LoginRequest userData)
    {
        var token = _userService.SignInAsync(userData);

        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
}