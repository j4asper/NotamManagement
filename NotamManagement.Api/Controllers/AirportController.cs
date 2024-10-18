using Microsoft.AspNetCore.Mvc;
using NotamManagement.Core.Models;

namespace NotamManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AirportController : ControllerBase
{
    [HttpGet("Id/{airportId:int}")]
    [ProducesResponseType<Airport>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult<Airport>> GetAirportByIdAsync(int airportId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("Id/{airportId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> DeleteAirportByIdAsync(int airportId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("Id/{airportId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> UpdateAirportByIdAsync(int airportId, Airport airport, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult<IReadOnlyList<Airport>>> GetAllAirportsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult> CreateAirportAsync(Airport airport, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
