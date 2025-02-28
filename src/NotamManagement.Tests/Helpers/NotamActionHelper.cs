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
            Id = 100,
            Importance = Importance.Default,
            NotamId = 8,
            Notam = new()
            {
                Id = 8,
                FIR = "FIR 1",
                Identifier = "Identifier 1",
                Location = "Location 1",
                Purpose = "Purpose 1",
                NotamOffice = "Notam Office 1",
                Scope = "Scope 1",
                Traffic = "Traffic 1",
                QCode = "Q Code 1",
                Coordinates = new()
                {
                    Id = 100,
                    Latitude = 1.1f,
                    Longitude = 1.1f
                },
            },
            Note = "Note 1",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 200,
            Importance = Importance.Default,
            NotamId = 2,
            Note = "Note 2",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 300,
            Importance = Importance.Default,
            NotamId = 3,
            Note = "Note 3",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 400,
            Importance = Importance.Default,
            NotamId = 4,
            Note = "Note 3",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 500,
            Importance = Importance.Default,
            NotamId = 5,
            Note = "Note 4",
            OrganizationId = 1
        },
        new NotamAction()
        {
            Id = 600,
            Importance = Importance.Default,
            NotamId = 6,
            Note = "Note 5",
            OrganizationId = 1
        }
    };
}
}