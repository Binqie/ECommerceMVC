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
        ViewBag.Error = null;
        return View();
    }
    
    [HttpPost]
    public IActionResult SignUp(CreateUserRequest userData)
    {
        var user = _userService.CreateUserAsync(userData);

        if (user is null)
        {
            ViewBag.Error = "User with this Email does already exist";
            return View();
        }

        return RedirectToAction("SignIn");
    }
    
    [HttpGet]
    public IActionResult SignIn()
    {
        ViewBag.Error = null;
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SignIn(LoginRequest userData)
    {
        var token = await _userService.SignInAsync(userData);

        if (token is null)
        {
            Console.WriteLine($"token: {token}");
            ViewBag.Error = "Email or Password is incorrect";
            return View();
        }

        return RedirectToAction("Index", "Home");
    }
}