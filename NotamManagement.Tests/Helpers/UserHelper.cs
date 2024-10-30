using NotamManagement.Core.Models;

namespace NotamManagement.Tests.Helpers;

public static class UserHelper
{
    public static IReadOnlyList<User> GetTestData()
{
        return new List<User>
    {
        new User()
        {
            FullName = "John Do",
            DateOfBirth = new DateTime(1980, 01, 01),
            OrganizationId = 1
        },
        new User()
        {
            FullName = "John Dont",
            DateOfBirth = new DateTime(1980, 01, 01),
            OrganizationId = 1
        },
        new User()
        {
            FullName = "John May",
            DateOfBirth = new DateTime(1980, 01, 01),
            OrganizationId = 1
        }
    };
}
}