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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NotamController(IRepository<Notam> notamRepository, IHttpContextAccessor httpContextAccessor)
    {
        _notamRepository = notamRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize]
    [HttpGet("Id/{notamId:int}")]
    [ProducesResponseType<Notam>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Notam>> GetNotamByIdAsync(int notamId, CancellationToken cancellationToken = default)
    {
        var notam = await _notamRepository.GetByIdAsync(notamId);
        return notam == null? NotFound() : Ok(notam);
      
    }
    
    [HttpGet("Unhandled/{organizationId:int}")]
    [ProducesResponseType<Notam>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<Notam>>> GetNotamsByOrganizationIdAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        var notams = await _notamRepository.GetAllUnhandledAsync(organizationId);
        return notams == null ? NotFound() : Ok(notams.ToList());
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
    
    [HttpGet("Stream")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async IAsyncEnumerable<Notam> GetAllNotamsAsAsyncEnumerable(CancellationToken cancellationToken = default)
    {
        int.TryParse(_httpContextAccessor.HttpContext?.User.FindFirst("OrganizationId")?.Value, out var orgId);
        
        await foreach (var item in _notamRepository.GetAllUnhandledAsAsyncEnumerable(orgId))
            yield return item;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateNotamAsync(Notam notam, CancellationToken cancellationToken = default)
    {
        await _notamRepository.AddAsync(notam);
        return Ok();
    }
}