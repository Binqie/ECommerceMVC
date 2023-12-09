using System.ComponentModel.DataAnnotations;
using System.Text;
using ECommerceMVC.Data;
using ECommerceMVC.Entities;
using ECommerceMVC.Models;
using ECommerceMVC.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Services;

public class UserService
{
    private readonly ApplicationContext _db;

    public UserService(ApplicationContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<List<UserResponse>> GetAllUsers()
    {
        var users = await _db.Users.Select(u => new UserResponse()
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Username = u.Username,
            CreatedAt = u.CreatedAt
        }).ToListAsync();

        return users;
    }

    public async Task<UserResponse?> GetUserById(int id)
    {
        var user = await _db.Users.Include(u => u.Orders).Select(u => new UserResponse()
        {
            Id = u.Id,
            Email = u.Email,
            Name = u.Name,
            Username = u.Username,
            CreatedAt = u.CreatedAt
        }).FirstOrDefaultAsync(u => u.Id == id);
        
        return user;
    }
    
    public async Task<UserResponse> CreateUserAsync(CreateUserRequest userData)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email == userData.Email || u.Username == userData.Username);
        if (user is not null)
        {
            throw new ValidationException("User with this Email of Username does already exist!");
        }

        User newUser = new User()
        {
            Name = userData.Name,
            Email = userData.Email,
            Username = userData.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(userData.Password)
        };

        await _db.Users.AddAsync(newUser);
        await _db.SaveChangesAsync();

        return new UserResponse()
        {
            Id = newUser.Id,
            Name = userData.Name,
            Email = userData.Email,
            Username = userData.Username
        };
    }

    public async Task<UserResponse> UpdateUserAsync(UpdateUserRequest userData)
    {
        var user = _db.Users.FirstOrDefault(u => u.Id == userData.Id);
        if (user is null)
        {
            throw new ValidationException("User does not exist!");
        }

        var newUser = new User()
        {
            Name = userData.Name,
            Email = userData.Email,
            Username = userData.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(userData.Password)
        };

        _db.Users.Update(newUser);
        await _db.SaveChangesAsync();

        return new UserResponse()
        {
            Id = newUser.Id,
            Name = userData.Name,
            Email = userData.Email,
            Username = userData.Username
        };
    }

    public async Task<UserResponse> DeleteUserAsync(int id)
    {
        var user = _db.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new ValidationException("User does not exist!");
        }

        var deletedUser = new UserResponse()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Username = user.Username,
            CreatedAt = user.CreatedAt
        };
        
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();

        return deletedUser;
    }

    // public async Task<UserResponse> SignInAsync(LoginRequest userData)
    // {
    //     var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == userData.Email);
    //     if (user is null)
    //     {
    //         throw new Exception("User does not exist!");
    //     }
    //     
    //     
    // }
}