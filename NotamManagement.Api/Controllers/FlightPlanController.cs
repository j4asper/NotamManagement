using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightPlanController : ControllerBase
{
    private readonly IRepository<FlightPlan> _flightPlanRepository;
    private readonly IRepository<Organization> _organizationRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<Airport> _airportRepository;


    public FlightPlanController(IRepository<FlightPlan> flightPlanRepository, IHttpContextAccessor httpContextAccessor, IRepository<Organization> organizationRepository, IRepository<Airport> airportRepository)
    {
        _flightPlanRepository = flightPlanRepository;
        _httpContextAccessor = httpContextAccessor;
        _organizationRepository = organizationRepository;
        _airportRepository = airportRepository;
    }


    [HttpGet("Id/{flightPlanId:int}")]
    [ProducesResponseType<FlightPlan>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightPlan>> GetFlightPlanByIdAsync(int flightPlanId, CancellationToken cancellationToken = default)
    {
        var flightPlan = await _flightPlanRepository.GetByIdAsync(flightPlanId);
        return flightPlan == null? NotFound() : Ok(flightPlan);
    }
    
    [HttpDelete("Id/{flightPlanId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteFlightPlanByIdAsync(int flightPlanId, CancellationToken cancellationToken = default)
    {
        await _flightPlanRepository.RemoveAsync(flightPlanId);
        return Ok();
    }
    
    [HttpPut("Id/{flightPlanId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateFlightPlanByIdAsync(int flightPlanId, FlightPlan flightPlan, CancellationToken cancellationToken = default)
    {
        await _flightPlanRepository.UpdateAsync(flightPlan);
        return Ok(flightPlan);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<FlightPlan>>> GetAllFlightPlansAsync(CancellationToken cancellationToken = default)
    {
        var orgID = _httpContextAccessor.HttpContext.User.FindFirst("OrganizationId").Value;
        var flightplan = await _organizationRepository.GetByIdAsync(int.Parse(orgID));
        var res = flightplan.FlightPlans;

        return Ok(res);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateFlightPlanAsync(FlightPlan flightPlan, CancellationToken cancellationToken = default)
    {



        var orgID = _httpContextAccessor.HttpContext.User.FindFirst("OrganizationId").Value;
        var organization = await _organizationRepository.GetByIdAsync(int.Parse(orgID));



        List<Airport> airports = new List<Airport>();
        foreach(var item in flightPlan.Airports)
        {
            var res = await _airportRepository.GetByIdAsync(item.Id);
            if(res!=null)
                airports.Add(res);
        }
        flightPlan.Airports = airports;
        organization.FlightPlans.Add(flightPlan);
        await _organizationRepository.UpdateAsync(organization);
        return Ok();
    }
}