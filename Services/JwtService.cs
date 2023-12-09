using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerceMVC.Data;
using ECommerceMVC.Entities;
using ECommerceMVC.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceMVC.Services;

public interface IJWTService
{
    Token Authenticate(LoginRequest user);
}

public class JwtService : IJWTService
{
    private readonly ApplicationContext _db;
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration, ApplicationContext db)
    {
        _configuration = configuration;
        _db = db;
    }
    public Token Authenticate(LoginRequest userData)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userData.Password);
        
        if (!_db.Users.Any(u => u.Email == userData.Email && u.Password == hashedPassword)) {
            return null;
        }
            
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userData.Email)                    
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token { AccessToken = tokenHandler.WriteToken(token) };

    }
}