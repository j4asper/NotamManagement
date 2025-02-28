using System.Text.Json.Serialization;

namespace NotamManagement.Core.Models;

public class FlightPlan
{
    public int Id { get; set; }
    
    public required List<Airport> Airports { get; set; }

    [JsonIgnore]
    public Airport Departure => Airports.First();
    
    [JsonIgnore]
    public Airport Destination => Airports.Last();
}