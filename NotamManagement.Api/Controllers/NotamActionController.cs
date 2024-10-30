using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using System.Security.Claims;
using System.Web;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotamActionController : ControllerBase
{
    private readonly IRepository<NotamAction> _notamActionRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NotamActionController(IRepository<NotamAction> notamActionRepository, IHttpContextAccessor httpContextAccessor)
    {
        _notamActionRepository = notamActionRepository;
        _httpContextAccessor = httpContextAccessor;
    }


    [HttpGet("Id/{notamActionId:int}")]
    [ProducesResponseType<NotamAction>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NotamAction>> GetNotamActionByIdAsync(int notamActionId, CancellationToken cancellationToken = default)
    {
        var action = await _notamActionRepository.GetByIdAsync(notamActionId);
        if(action == null)
        {
            return NotFound();
        }
        return Ok(action);
        

    }
    
    [HttpDelete("Id/{notamActionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteNotamActionByIdAsync(int notamActionId, CancellationToken cancellationToken = default)
    {
        await _notamActionRepository.RemoveAsync(notamActionId);
        return Ok();
    }
    
    [HttpPut("Id/{notamActionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateNotamActionByIdAsync(int notamActionId, NotamAction notamAction, CancellationToken cancellationToken = default)
    {
        var nAction = await _notamActionRepository.GetByIdAsync(notamActionId);
        if(nAction == null)
        {
            return NotFound();
        }
        await _notamActionRepository.UpdateAsync(notamAction);
        return Ok(nAction);
    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<NotamAction>>> GetAllNotamActionsAsync(CancellationToken cancellationToken = default)
    {
        var orgID = _httpContextAccessor.HttpContext.User.FindFirst("OrganizationId").Value;
        var notams = await _notamActionRepository.GetAllAsync(int.Parse(orgID));
        return Ok(notams);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateNotamActionAsync(NotamAction notamAction, CancellationToken cancellationToken = default)
    {
         if(notamAction == null)
        {
            return BadRequest();
        }
        await _notamActionRepository.AddAsync(notamAction);
        return Ok(notamAction);
    }
}