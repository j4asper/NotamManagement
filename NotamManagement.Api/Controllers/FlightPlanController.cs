using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightPlanController : ControllerBase
{
    private readonly IRepository<FlightPlan> _flightPlanRepository;


    public FlightPlanController(IRepository<FlightPlan> flightPlanRepository)
    {
        _flightPlanRepository = flightPlanRepository;
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
        var flightPlans = await _flightPlanRepository.GetAllAsync();
        return Ok(flightPlans);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateFlightPlanAsync(FlightPlan flightPlan, CancellationToken cancellationToken = default)
    {
        await _flightPlanRepository.AddAsync(flightPlan);
        return Ok(flightPlan);
    }
}