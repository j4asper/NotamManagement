using NotamManagement.Core.Models;

namespace NotamManagement.Tests.Helpers;

public static class AirportHelper
{
    public static IReadOnlyList<Airport> GetTestData()
{
        return new List<Airport>
    {
        new Airport()
        {
            Id = 1,
            FIR = "EKDK",
            ICAO = "EKCH",
        },
        new Airport()
        {
            Id = 2,
            FIR = "EKDK",
            ICAO = "EKBI",
        },
        new Airport()
        {
            Id = 3,
            FIR = "EKDK",
            ICAO = "EKRK",
        },
        new Airport()
        {
            Id = 4,
            FIR = "EKDK",
            ICAO = "EKOD",
        }
    };
}
}