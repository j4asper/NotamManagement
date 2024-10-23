using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IRepository<User> _userRepository;

    public UserController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }


    [HttpGet("Id/{userId:int}")]
    [ProducesResponseType<User>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult<User>> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("Id/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> DeleteUserByIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("Id/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> UpdateUserByIdAsync(int userId, User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<User>>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync();
        return users == null ? [] : users.ToList();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
