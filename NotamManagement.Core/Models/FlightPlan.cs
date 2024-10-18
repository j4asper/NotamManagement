namespace NotamManagement.Shared.Models;

public class FlightPlan
{
    public int Id { get; set; }
    
    public List<Airport> Airports { get; set; }
}