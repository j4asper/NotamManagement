namespace NotamManagement.Core.Models;

public class Airport
{
    public int Id { get; set; }
    
    public required string ICAO { get; set; }
    
    public required string FIR { get; set; }

    public List<FlightPlan>? FlightPlans { get; set; }
}