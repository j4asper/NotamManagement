using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Core.RepositoryTests;

public class AirportRepositoryTests
{
    private readonly IReadOnlyList<Airport> airports;
    private readonly NotamManagementContext context;

    public AirportRepositoryTests()
    {
        context = DatabaseHelper.GetInMemoryDbContext();
        airports = AirportHelper.GetTestData();
    }
    
    [Fact]
    public async Task AddAirport_ShouldAddAirport()
    {
        var repository = new AirportRepository(context);
        
        // Arrange
        var airport = airports[0];

        // Act
        await repository.AddAsync(airport);

        // Assert
        var result = await repository.FindAsync(x => x.Id == airport.Id);
        
        Assert.NotNull(result.First());
        Assert.Equal(airport.ICAO, result.First().ICAO);
    }

    [Fact]
    public async Task GetNotam_ShouldReturnNull_WhenNotFound()
    {
        var repository = new AirportRepository(context);
        
        // Act
        var result = await repository.FindAsync(x => x.Id == int.MaxValue); // ID that doesn't exist

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetAirportById_ShouldReturnAirport()
    {
        var repository = new AirportRepository(context);
        
        // Arrange
        var airport = airports[0];
        
        await repository.AddAsync(airport);
        
        // Act
        var result = await repository.GetByIdAsync(airport.Id); // ID that doesn't exist

        // Assert
        Assert.Equal(airport.Id, result.Id);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveAirport()
    {
        var repository = new AirportRepository(context);
        
        // Arrange
        var airport = airports[0];
        await repository.AddAsync(airport);

        // Act
        await repository.RemoveAsync(airport.Id);

        // Assert
        var result = await repository.FindAsync(x => x.Id == airport.Id);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task RemoveByObjectAsync_ShouldRemoveAirport()
    {
        var repository = new AirportRepository(context);
        
        // Arrange
        var airport = airports[0];
        await repository.AddAsync(airport);

        // Act
        await repository.RemoveAsync(airport);

        // Assert
        var result = await repository.FindAsync(x => x.Id == airport.Id);
        Assert.Empty(result);
    }


    [Fact]
    public async Task UpdateAsync_ShouldUpdateAirport()
    {
        var repository = new AirportRepository(context);
        
        // Arrange
        var airport = airports[0];
        await repository.AddAsync(airport);
        airport.ICAO = "TEST";

        // Act
        await repository.UpdateAsync(airport);

        // Assert
        var result = await repository.GetByIdAsync(airport.Id);
        Assert.Equal("TEST", result.ICAO);
    }
}