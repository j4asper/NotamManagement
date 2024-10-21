namespace NotamManagement.Core.Models;

public class Coordinates
{
    public int Id { get; set; }

    public int NotamId { get; set; }
    public float Longitude { get; set; }
    
    public float Latitude { get; set; }
    
    public int UpperFlightLevel { get; set; }
    
    public int LowerFlightLevel { get; set; }

    public int Radius { get; set; }

    //make a prop that gives long and lat in the format XXXXNXXXXXW or XXXXNXXXXXE
    public string LongLat => DecimalToDMS();



    private string DecimalToDMS()
    {
        // Latitude conversion
        char latDir = Latitude >= 0 ? 'N' : 'S';
        Latitude = Math.Abs(Latitude);
        int latDeg = (int)Latitude;
        int latMin = (int)((Latitude - latDeg) * 60);

        // Longitude conversion
        char lonDir = Longitude >= 0 ? 'E' : 'W';
        Longitude = Math.Abs(Longitude);
        int lonDeg = (int)Longitude;
        int lonMin = (int)((Longitude - lonDeg) * 60);

        // Return formatted string
        return $"{latDeg:00}{latMin:00}{latDir}{lonDeg:000}{lonMin:00}{lonDir}";
    }

}