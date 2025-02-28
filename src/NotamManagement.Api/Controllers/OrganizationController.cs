using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class OrganizationController : ControllerBase
{

    private readonly IRepository<Organization> _organizationRepository;

    public OrganizationController(IRepository<Organization> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }


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
    public async Task<ActionResult> UpdateOrganizationByIdAsync(int organizationId, Organization organization, CancellationToken cancellationToken = default)
    {
        organization.Id = organizationId;
       await _organizationRepository.UpdateAsync(organization);
        return Ok();
      
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
    public async Task<ActionResult> CreateOrganizationAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        if(organization == null)
        {
            return BadRequest();
        }
        else
        {
            await _organizationRepository.AddAsync(organization);
            return Ok();
        }
    }
}
