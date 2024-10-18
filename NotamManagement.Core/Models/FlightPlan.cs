namespace NotamManagement.Core.Models;

public class FlightPlan
{
    public required int Id { get; set; }
    
    public required List<Airport> Airports { get; set; }
}