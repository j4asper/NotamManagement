using NotamManagement.Core.Models;

namespace NotamManagement.Tests.Helpers;

public static class FlightPlanHelper
{
    public static IReadOnlyList<FlightPlan> GetTestData()
{
        return new List<FlightPlan>
    {
        new FlightPlan()
        {
            Id = 1,
            Airports = AirportHelper.GetTestData().ToList()
        },
        new FlightPlan()
        {
            Id = 2,
            Airports = AirportHelper.GetTestData().ToList()
        },
        new FlightPlan()
        {
            Id = 3,
            Airports = AirportHelper.GetTestData().ToList()
        },
        new FlightPlan()
        {
            Id = 4,
            Airports = AirportHelper.GetTestData().ToList()
        }
    };
}
}