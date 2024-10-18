using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationController : ControllerBase
{
    [HttpGet("Id/{organizationId:int}")]
    [ProducesResponseType<Organization>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult<Organization>> GetOrganizationByIdAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("Id/{organizationId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> DeleteOrganizationByIdAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("Id/{organizationId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> UpdateOrganizationByIdAsync(int organizationId, Organization organization, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult<IReadOnlyList<Organization>>> GetAllOrganizationsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult> CreateOrganizationAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
