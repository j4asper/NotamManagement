using NotamManagement.Core.Models;

namespace NotamManagement.Tests.Helpers;

public static class NotamHelper
{
    public static IReadOnlyList<Notam> GetTestData()
{
        return new List<Notam>
    {
        new Notam
        {
            Id = 1,
            Location = "EKCH",
            ValidFrom = new DateTime(2024, 1, 1),
            ValidTo = new DateTime(2024, 12, 31),
            EffectiveValidTo = new DateTime(2024, 12, 31),
            Type = NotamType.New,
            NotamOffice = "EKCH",
            QCode = "FPXX",
            Identifier = "D102/24",
            ReferenceIdentifier = "D101/24",
            FIR = "EKDK",
            Purpose = "BO",
            Traffic = "IV",
            Scope = "A",
            NotamText = "SOUTH WEST FLIGHT SECTOR IS REDUCED BY 29 DEGREES TO 121 DEGREES",
            Coordinates = new Coordinates
            {
                Id = 1,
                NotamId = 1,
                Longitude = (float)12.56667,
                Latitude = (float)55.7,
                UpperFlightLevel = 999,
                LowerFlightLevel = 0,
                Radius = 5
            }
        },
        new Notam
        {
            Id = 2,
            Location = "EKDK",
            ValidFrom = new DateTime(2024, 2, 1),
            ValidTo = new DateTime(2024, 6, 30),
            EffectiveValidTo = null,
            Type = NotamType.Revision,
            NotamOffice = "EKCH",
            QCode = "FPYY",
            Identifier = "D203/24",
            ReferenceIdentifier = "D202/24",
            FIR = "EKDK",
            Purpose = "BO",
            Traffic = "IV",
            Scope = "A",
            NotamText = "NEW RESTRICTIONS IN FLIGHT SECTOR.",
            Coordinates = new Coordinates
            {
                Id = 2,
                NotamId = 2,
                Longitude = (float)12.5,
                Latitude = (float)55.6,
                UpperFlightLevel = 500,
                LowerFlightLevel = 100,
                Radius = 10
            }
        },
        new Notam
        {
            Id = 3,
            Location = "EKCH",
            ValidFrom = new DateTime(2024, 3, 1),
            ValidTo = new DateTime(2024, 11, 30),
            EffectiveValidTo = null,
            Type = NotamType.New,
            NotamOffice = "EKCH",
            QCode = "FPZZ",
            Identifier = "D304/24",
            ReferenceIdentifier = "D303/24",
            FIR = "EKDK",
            Purpose = "BO",
            Traffic = "IV",
            Scope = "A",
            NotamText = "FLIGHT SECTOR CLOSED DUE TO MAINTENANCE.",
            Coordinates = new Coordinates
            {
                Id = 3,
                NotamId = 3,
                Longitude = (float)12.4,
                Latitude = (float)55.5,
                UpperFlightLevel = 300,
                LowerFlightLevel = 50,
                Radius = 15
            }
        },
        new Notam
        {
            Id = 4,
            Location = "EKDK",
            ValidFrom = new DateTime(2024, 4, 1),
            ValidTo = new DateTime(2024, 10, 31),
            EffectiveValidTo = null,
            Type = NotamType.New,
            NotamOffice = "EKCH",
            QCode = "FPWW",
            Identifier = "D405/24",
            ReferenceIdentifier = null,
            FIR = "EKDK",
            Purpose = "BO",
            Traffic = "IV",
            Scope = "A",
            NotamText = "NEW RESTRICTIONS IN FLIGHT SECTOR.",
            Coordinates = new Coordinates
            {
                Id = 4,
                NotamId = 4,
                Longitude = (float)12.3,
                Latitude = (float)55.4,
                UpperFlightLevel = 400,
                LowerFlightLevel = 0,
                Radius = 20
            }
        },
        new Notam{
            Id = 5,
            Location = "EKCH",
            ValidFrom = new DateTime(2024, 5, 1),
            ValidTo = new DateTime(2024, 9, 30),
            EffectiveValidTo = null,
            Type = NotamType.Revision,
            NotamOffice = "EKCH",
            QCode = "FPVV",
            Identifier = "D506/24",
            ReferenceIdentifier = "D405/24",
            FIR = "EKDK",
            Purpose = "BO",
            Traffic = "IV",
            Scope = "A",
            NotamText = "NEW RESTRICTIONS IN FLIGHT SECTOR.",
            Coordinates = new Coordinates
            {
                Id = 5,
                NotamId = 5,
                Longitude = (float)12.2,
                Latitude = (float)55.3,
                UpperFlightLevel = 500,
                LowerFlightLevel = 100,
                Radius = 25
            }
        },
        new Notam
        {
            Id = 6,
            Location = "EKCH",
            ValidFrom = new DateTime(2024, 5, 1),
            ValidTo = new DateTime(2024, 9, 30),
            EffectiveValidTo = null,
            Type = NotamType.Cancellation,
            NotamOffice = "EKCH",
            QCode = "FPVV",
            Identifier = "D507/24",
            ReferenceIdentifier = "D506/24",
            FIR = "EKDK",
            Purpose = "BO",
            Traffic = "IV",
            Scope = "A",
            NotamText = "NEW RESTRICTIONS IN FLIGHT SECTOR.",
            Coordinates = new Coordinates
            {
                Id = 6,
                NotamId = 6,
                Longitude = (float)12.2,
                Latitude = (float)55.3,
                UpperFlightLevel = 500,
                LowerFlightLevel = 100,
                Radius = 25
            }
        },
        new Notam
        {
            Id = 12,
            Location = "EKCH",
            ValidFrom = new DateTime(2024, 5, 1),
            ValidTo = new DateTime(2024, 9, 30),
            EffectiveValidTo = null,
            Type = NotamType.New,
            NotamOffice = "EKCH",
            QCode = "FPVV",
            Identifier = "D555/24",
            ReferenceIdentifier = null,
            FIR = "EKDK",
            Purpose = "BO",
            Traffic = "IV",
            Scope = "A",
            NotamText = "NEW RESTRICTIONS IN FLIGHT SECTOR.",
            Coordinates = new Coordinates
            {
                Id = 12,
                NotamId = 12,
                Longitude = (float)12.2,
                Latitude = (float)55.3,
                UpperFlightLevel = 500,
                LowerFlightLevel = 100,
                Radius = 25
            }
        }
    };
}
}