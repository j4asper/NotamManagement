using NotamManagement.Core.Models;

namespace NotamManagement.Tests.Helpers;

public static class NotamActionHelper
{
    public static IReadOnlyList<NotamAction> GetTestData()
{
        return new List<NotamAction>
    {
        new NotamAction()
        {
            Id = 1,
            Importance = Importance.Default,
            NotamId = 1,
            Note = "Note 1",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 2,
            Importance = Importance.Default,
            NotamId = 2,
            Note = "Note 2",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 3,
            Importance = Importance.Default,
            NotamId = 3,
            Note = "Note 3",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 3,
            Importance = Importance.Default,
            NotamId = 3,
            Note = "Note 3",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 4,
            Importance = Importance.Default,
            NotamId = 4,
            Note = "Note 4",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 5,
            Importance = Importance.Default,
            NotamId = 5,
            Note = "Note 5",
            OrganizationId = 5
        }
    };
}
}