using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotamController : ControllerBase
{
    private readonly IRepository<Notam> _notamRepository;

    public NotamController(IRepository<Notam> notamRepository)
    {
        _notamRepository = notamRepository;
    }

    [Authorize]
    [HttpGet("Id/{notamId:int}")]
    [ProducesResponseType<Notam>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Notam>> GetNotamByIdAsync(int notamId, CancellationToken cancellationToken = default)
    {
        var notam = await _notamRepository.GetByIdAsync(notamId);
        return notam == null? NotFound() : notam;
      
    }
    
    [HttpDelete("Id/{notamId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteNotamByIdAsync(int notamId, CancellationToken cancellationToken = default)
    {
         await _notamRepository.RemoveAsync(notamId);
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<Notam>>> GetAllNotamsAsync(CancellationToken cancellationToken = default)
    {
        var notams = await _notamRepository.GetAllAsync();
        
        return Ok(notams);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateNotamAsync(Notam notam, CancellationToken cancellationToken = default)
    {
        await _notamRepository.AddAsync(notam);
        return Ok();

    }
}