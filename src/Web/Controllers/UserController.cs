using CreationHub.Models.NicePartUsage;
using CreationHub.Web.Dtos;
using Domain.User;
using Microsoft.AspNetCore.Mvc;

namespace CreationHub.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly CreationHubContext _context;
    
    public UserController(CreationHubContext context)
    {
        _context = context;
    }
    
    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(long id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return UserToDto(user);
    }
    
    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<UserDto>> PostRating(UserDto userDto)
    {
        var user = new User
        {
            Email = userDto.Email,
            Password = userDto.Password,
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    
    private static UserDto UserToDto(User user) => 
        new UserDto
        {
            Id = user.Id,
            Email = user.Email
        };
}