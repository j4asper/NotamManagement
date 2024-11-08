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
    public async Task GetAllAirports_ShouldReturnAllAirports()
    {
        var repository = new AirportRepository(context);
        
        // Arrange
        await repository.AddAsync(airports[0]);
        
        // Act
        var result = await repository.GetAllAsync(null); // ID that doesn't exist

        // Assert
        Assert.Equal(1, result.Count);
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
    
    [Fact]
    public async Task RemoveRangeAsync_ShouldRemoveAirports()
    {
        var repository = new AirportRepository(context);
        
        // Arrange
        await repository.AddAsync(airports.First());

        // Act
        await repository.RemoveRangeAsync([airports.First()]);

        // Assert
        var result = await repository.FindAsync(x => x.Id == airports.First().Id);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetAllUnhandledAsAsyncEnumerable_ShouldThrowNotImplementedException()
    {
        var repository = new AirportRepository(context);
        
        Assert.Throws<NotImplementedException>(() => repository.GetAllUnhandledAsAsyncEnumerable(0));
    }
    
    [Fact]
    public async Task GetAllUnhandledAsync_ShouldThrowNotImplementedException()
    {
        var repository = new AirportRepository(context);
        
        await Assert.ThrowsAsync<NotImplementedException>(() => repository.GetAllUnhandledAsync(0));
    }
    
    [Fact]
    public async Task AddRangeAsync_ShouldThrowNotImplementedException()
    {
        var repository = new AirportRepository(context);
        
        await Assert.ThrowsAsync<NotImplementedException>(() => repository.AddRangeAsync([]));
    }
    
    [Fact]
    public async Task GetAllAsAsyncEnumerable_ShouldThrowNotImplementedException()
    {
        var repository = new AirportRepository(context);
        
        Assert.Throws<NotImplementedException>(() => repository.GetAllAsAsyncEnumerable(0));
    }
}