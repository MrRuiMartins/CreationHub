using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CreationHub.Models.NicePartUsage;
using CreationHub.Web.Dtos;
using Domain.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CreationHub.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly CreationHubContext _context;

    public AuthController(CreationHubContext context)
    {
        _context = context;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {

        var user = await _context.Users
            .Where(u => u.Email == request.Email && u.Password == request.Password)
            .FirstAsync();

        if (user?.Email == null || user.Role == null)
        {
            return Unauthorized("Invalid email or password");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("SecretKey-SecretKey-SecretKey-SecretKey-SecretKey");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new { Token = tokenHandler.WriteToken(token) });
    }
}