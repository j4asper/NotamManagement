﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using NotamManagement.Api.Controllers;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Api;

public class AirportControllerTests
{
    private readonly IReadOnlyList<Airport> airports;
    private readonly Mock<IRepository<Airport>> mockRepository;
    private readonly AirportController controller;

    public AirportControllerTests()
    {
        mockRepository = new Mock<IRepository<Airport>>();
        controller = new AirportController(mockRepository.Object);
        airports = AirportHelper.GetTestData();
    }
    
    [Fact]
    public async Task GetAirportByIdAsync_ReturnsNotFound_WhenAirportDoesNotExist()
    {
        // Arrange
        int airportId = 999; // ID that doesn't exist
        mockRepository.Setup(repo => repo.GetByIdAsync(airportId))
            .ReturnsAsync((Airport)null); // Simulate not found

        // Act
        var result = await controller.GetAirportByIdAsync(airportId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetAirportByIdAsync_ReturnsAirport_WhenAirportExists()
    {
        // Arrange
        var airport = airports[0];
        
        mockRepository.Setup(repo => repo.GetByIdAsync(airport.Id))
            .ReturnsAsync(airport); // Simulate found

        // Act
        var result = await controller.GetAirportByIdAsync(airport.Id);
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var airportResult = okResult.Value as Airport;
        Assert.Equal(airport.Id, airportResult.Id);
    }

    [Fact]
    public async Task DeleteAirportByIdAsync_ReturnsOk_WhenAirportExists()
    {
        // Arrange
        int airportId = 1;
        
        mockRepository.Setup(repo => repo.RemoveAsync(airportId))
            .Returns(Task.CompletedTask); // Simulate successful deletion

        // Act
        var result = await controller.DeleteAirportByIdAsync(airportId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteAirportByIdAsync_ReturnsNotFound_WhenAirportDoesNotExist()
    {
        // Arrange
        int airportId = 999; // ID that doesn't exist
        mockRepository.Setup(repo => repo.RemoveAsync(airportId))
            .Returns(Task.CompletedTask); // Simulate not found

        // Act
        var result = await controller.DeleteAirportByIdAsync(airportId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task GetAllAirportsAsync_ReturnsListOfAirports()
    {
        // Arrange
        mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(airports); // Return the predefined list

        // Act
        var result = await controller.GetAllAirportsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var airportList = okResult.Value as IReadOnlyList<Airport>;
        Assert.NotNull(airportList);
        Assert.Equal(airports.Count, airportList.Count);
    }

    [Fact]
    public async Task CreateAirportAsync_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var airport = airports[0];
        
        mockRepository.Setup(repo => repo.AddAsync(airport))
            .Returns(Task.CompletedTask); // Simulate successful addition

        // Act
        var result = await controller.CreateAirportAsync(airport);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var airportResult = okResult.Value as Airport;
        Assert.Equal(airport.Id, airportResult.Id);
    }
}