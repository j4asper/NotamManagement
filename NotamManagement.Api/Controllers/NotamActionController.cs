using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotamActionController : ControllerBase
{
    private readonly IRepository<NotamAction> _notamActionRepository;

    public NotamActionController(IRepository<NotamAction> notamActionRepository)
    {
        _notamActionRepository = notamActionRepository;
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
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<NotamAction>>> GetAllNotamActionsAsync(CancellationToken cancellationToken = default)
    {
       var notams = await _notamActionRepository.GetAllAsync();
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