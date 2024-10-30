using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AirportController : ControllerBase
{

    private readonly IRepository<Airport> _airportRepository;

    public AirportController(IRepository<Airport> airportRepository)
    {
        _airportRepository = airportRepository;
    }
    
    [HttpGet("Id/{airportId:int}")]
    [ProducesResponseType<Airport>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Airport>> GetAirportByIdAsync(int airportId, CancellationToken cancellationToken = default)
    {
        var airport = await _airportRepository.GetByIdAsync(airportId);
        return Ok(airport);
    }
    
    [HttpDelete("Id/{airportId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAirportByIdAsync(int airportId, CancellationToken cancellationToken = default)
    {
        await _airportRepository.RemoveAsync(airportId);
        return Ok();
    }
    
    [HttpPut("Id/{airportId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAirportByIdAsync(int airportId, Airport airport, CancellationToken cancellationToken = default)
    {
        var airPort = await _airportRepository.GetByIdAsync(airportId);
        if(airPort == null)
        {
            return NotFound();
        }
        await _airportRepository.UpdateAsync(airport);
        return Ok(airPort);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<Airport>>> GetAllAirportsAsync(CancellationToken cancellationToken = default)
    {
        var airports = await _airportRepository.GetAllAsync();
        return Ok(airports);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateAirportAsync(Airport airport, CancellationToken cancellationToken = default)
    {
        await _airportRepository.AddAsync(airport);
        return Ok(airport);
    }
}
