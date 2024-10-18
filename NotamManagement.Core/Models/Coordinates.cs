namespace NotamManagement.Core.Models;

public class Coordinates
{
    public int Id { get; set; }

    public int NotamId { get; set; }
    public required Notam Notam { get; set; }
    public float Longitude { get; set; }
    
    public float Latitude { get; set; }
    
    public int UpperFlightLevel { get; set; }
    
    public int LowerFlightLevel { get; set; }

    public int Radius { get; set; }
}