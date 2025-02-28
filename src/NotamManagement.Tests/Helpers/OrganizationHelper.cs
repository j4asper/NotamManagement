using NotamManagement.Core.Models;

namespace NotamManagement.Tests.Helpers;

public static class OrganizationHelper
{
    public static IReadOnlyList<Organization> GetTestData()
{
        return new List<Organization>
    {
        new Organization()
        {
            Id = 1,
            FlightPlans = [FlightPlanHelper.GetTestData().First()],
            Name = "SAS",
            NotamActions = [NotamActionHelper.GetTestData().First()],
            Users = [UserHelper.GetTestData().First()]
        },
        new Organization()
        {
            Id = 2,
            FlightPlans = [FlightPlanHelper.GetTestData().Last()],
            Name = "EasyJet",
            NotamActions = [NotamActionHelper.GetTestData().Last()],
            Users = [UserHelper.GetTestData().Last()]
        }
    };
}
}