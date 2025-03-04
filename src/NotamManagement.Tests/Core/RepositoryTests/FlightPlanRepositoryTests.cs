﻿using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Core.RepositoryTests;

public class FlightPlanRepositoryTests
{
    private readonly IReadOnlyList<FlightPlan> flightPlans;
    private readonly NotamManagementContext context;

    public FlightPlanRepositoryTests()
    {
        context = DatabaseHelper.GetInMemoryDbContext();
        flightPlans = FlightPlanHelper.GetTestData();
    }
    
    [Fact]
    public async Task AddFlightPlan_ShouldAddFlightPlan()
    {
        var repository = new FlightPlanRepository(context);
        
        // Arrange
        var flightPlan = flightPlans[0];

        // Act
        await repository.AddAsync(flightPlan);

        // Assert
        var result = await repository.FindAsync(x => x.Id == flightPlan.Id);
        
        Assert.NotNull(result.First());
        Assert.Equal(flightPlan.Destination, result.First().Destination);
    }

    [Fact]
    public async Task GetFlightPlan_ShouldReturnNull_WhenNotFound()
    {
        var repository = new FlightPlanRepository(context);
        
        // Act
        var result = await repository.FindAsync(x => x.Id == int.MaxValue); // ID that doesn't exist

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetFlightPlanById_ShouldReturnFlightPlan()
    {
        var repository = new FlightPlanRepository(context);
        
        // Arrange
        var flightPlan = flightPlans[0];
        
        await repository.AddAsync(flightPlan);
        
        // Act
        var result = await repository.GetByIdAsync(flightPlan.Id); // ID that doesn't exist

        // Assert
        Assert.Equal(flightPlan.Id, result.Id);
    }
    
    [Fact]
    public async Task GetAllFlightPlans_ShouldReturnAllFlightPlans()
    {
        var repository = new FlightPlanRepository(context);
        
        // Arrange
        await repository.AddAsync(flightPlans[0]);
        
        // Act
        var result = await repository.GetAllAsync(null); // ID that doesn't exist

        // Assert
        Assert.Equal(1, result.Count);
    }
    
    [Fact]
    public async Task RemoveAsync_ShouldRemoveFlightPlan()
    {
        var repository = new FlightPlanRepository(context);
        
        // Arrange
        var flightPlan = flightPlans[0];
        await repository.AddAsync(flightPlan);

        // Act
        await repository.RemoveAsync(flightPlan.Id);

        // Assert
        var result = await repository.FindAsync(x => x.Id == flightPlan.Id);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task RemoveByObjectAsync_ShouldRemoveFlightPlan()
    {
        var repository = new FlightPlanRepository(context);
        
        // Arrange
        var flightPlan = flightPlans[0];
        await repository.AddAsync(flightPlan);

        // Act
        await repository.RemoveAsync(flightPlan);

        // Assert
        var result = await repository.FindAsync(x => x.Id == flightPlan.Id);
        Assert.Empty(result);
    }

    [Fact]
    public async Task RemoveRangeAsync_ShouldRemoveFlightPlans()
    {
        var repository = new FlightPlanRepository(context);
        
        // Arrange
        await repository.AddAsync(flightPlans.First());

        // Act
        await repository.RemoveRangeAsync([flightPlans.First()]);

        // Assert
        var result = await repository.FindAsync(x => x.Id == flightPlans.First().Id);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateFlightPlan()
    {
        var repository = new FlightPlanRepository(context);
        
        // Arrange
        var flightPlan = flightPlans[0];
        await repository.AddAsync(flightPlan);
        
        flightPlan.Airports.RemoveAt(0);

        // Act
        await repository.UpdateAsync(flightPlan);

        // Assert
        var result = await repository.GetByIdAsync(flightPlan.Id);
        Assert.NotEqual(1, result.Airports[0].Id);
    }
    
    [Fact]
    public async Task GetAllUnhandledAsAsyncEnumerable_ShouldThrowNotImplementedException()
    {
        var repository = new FlightPlanRepository(context);
        
        Assert.Throws<NotImplementedException>(() => repository.GetAllUnhandledAsAsyncEnumerable(0));
    }
    
    [Fact]
    public async Task GetAllUnhandledAsync_ShouldThrowNotImplementedException()
    {
        var repository = new FlightPlanRepository(context);
        
        await Assert.ThrowsAsync<NotImplementedException>(() => repository.GetAllUnhandledAsync(0));
    }
    
    [Fact]
    public async Task GetAllAsAsyncEnumerable_ShouldThrowNotImplementedException()
    {
        var repository = new FlightPlanRepository(context);
        
        Assert.Throws<NotImplementedException>(() => repository.GetAllAsAsyncEnumerable(0));
    }
}