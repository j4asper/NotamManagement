using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository<User> _userRepository;

    public UserController(IUserRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }


    [HttpGet("Id/{userId:int}")]
    [ProducesResponseType<User>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
       var user = await _userRepository.GetByIdAsync(userId);
        if(user == null)
        {
            return NotFound();
        }
        return user;
    }
    
    [HttpDelete("Id/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteUserByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
       var user = await _userRepository.GetByIdAsync(userId);
        if(user == null)
        {
            return NotFound();
        }
        await _userRepository.RemoveAsync(userId);
        return Ok();
    }

    [HttpGet("isAuth")]
    public IActionResult IsAuthenticated()
    {
        // Check if the user is authenticated
        if (User.Identity?.IsAuthenticated == true)
        {
            return Ok(true);  // Return true if the user is authenticated
        }

        return Ok(false); // Return false if not authenticated
    }


    [HttpPatch("Id/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateUserByIdAsync(string userId, [FromBody] User user, CancellationToken cancellationToken = default)
    {
        var currUser = await _userRepository.GetByIdAsync(userId);
        if(currUser == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(user.FullName))
        {
            currUser.FullName = user.FullName;
        }
        if(!string.IsNullOrEmpty(user.Email))
        {
            currUser.Email = user.Email;
            currUser.NormalizedEmail = user.Email.ToUpper();
        }
        if(!string.IsNullOrEmpty(user.PhoneNumber))
        {
            currUser.PhoneNumber = user.PhoneNumber;
        }
        if(!string.IsNullOrEmpty(user.UserName))
        {
            currUser.UserName = user.UserName;
            currUser.NormalizedUserName = user.UserName.ToUpper();
        }
        if(!string.IsNullOrEmpty(user.DateOfBirth.ToString()))
        {
            currUser.DateOfBirth = user.DateOfBirth;
        }
        if (!string.IsNullOrEmpty(user.OrganizationId.ToString()))
        {
            currUser.OrganizationId = user.OrganizationId;
        }
        
      

        await _userRepository.UpdateAsync(currUser);
        return Ok();
        

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
