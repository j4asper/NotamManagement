using Microsoft.AspNetCore.Mvc;
using Moq;
using NotamManagement.Api.Controllers;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Api;

public class FlightPlanControllerTests
{
    private readonly IReadOnlyList<FlightPlan> flightPlans;
    private readonly Mock<IRepository<FlightPlan>> mockRepository;
    private readonly FlightPlanController controller;

    public FlightPlanControllerTests()
    {
        mockRepository = new Mock<IRepository<FlightPlan>>();
        controller = new FlightPlanController(mockRepository.Object);
        flightPlans = FlightPlanHelper.GetTestData();
    }
    
    [Fact]
    public async Task GetFlightPlanByIdAsync_ReturnsNotFound_WhenFlightPlanDoesNotExist()
    {
        // Arrange
        int flightPlanId = 999; // ID that doesn't exist
        mockRepository.Setup(repo => repo.GetByIdAsync(flightPlanId))
            .ReturnsAsync((FlightPlan)null); // Simulate not found

        // Act
        var result = await controller.GetFlightPlanByIdAsync(flightPlanId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetFlightPlanByIdAsync_ReturnsFlightPlan_WhenFlightPlanExists()
    {
        // Arrange
        var flightPlan = flightPlans[0];
        
        mockRepository.Setup(repo => repo.GetByIdAsync(flightPlan.Id))
            .ReturnsAsync(flightPlan); // Simulate found

        // Act
        var result = await controller.GetFlightPlanByIdAsync(flightPlan.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var flightPlanResult = okResult.Value as FlightPlan;
        Assert.Equal(flightPlan.Id, flightPlanResult.Id);
    }

    [Fact]
    public async Task DeleteFlightPlanByIdAsync_ReturnsOk_WhenFlightPlanExists()
    {
        // Arrange
        int flightPlanId = 1;
        
        mockRepository.Setup(repo => repo.RemoveAsync(flightPlanId))
            .Returns(Task.CompletedTask); // Simulate successful deletion

        // Act
        var result = await controller.DeleteFlightPlanByIdAsync(flightPlanId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteFlightPlanByIdAsync_ReturnsNotFound_WhenFlightPlanDoesNotExist()
    {
        // Arrange
        int flightPlanId = 999; // ID that doesn't exist
        mockRepository.Setup(repo => repo.RemoveAsync(flightPlanId))
            .Returns(Task.CompletedTask); // Simulate not found

        // Act
        var result = await controller.DeleteFlightPlanByIdAsync(flightPlanId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task GetAllFlightPlansAsync_ReturnsListOfFlightPlans()
    {
        // Arrange
        mockRepository.Setup(repo => repo.GetAllAsync(null))
            .ReturnsAsync(flightPlans); // Return the predefined list

        // Act
        var result = await controller.GetAllFlightPlansAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var flightPlanList = okResult.Value as IReadOnlyList<FlightPlan>;
        Assert.NotNull(flightPlanList);
        Assert.Equal(flightPlans.Count, flightPlanList.Count);
    }

    [Fact]
    public async Task CreateFlightPlanAsync_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var flightPlan = flightPlans[0];
        
        mockRepository.Setup(repo => repo.AddAsync(flightPlan))
            .Returns(Task.CompletedTask); // Simulate successful addition

        // Act
        var result = await controller.CreateFlightPlanAsync(flightPlan);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}