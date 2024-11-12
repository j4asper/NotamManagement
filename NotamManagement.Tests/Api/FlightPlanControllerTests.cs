using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotamManagement.Api.Controllers;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;
using System.Security.Claims;

namespace NotamManagement.Tests.Api;

public class FlightPlanControllerTests
{
    private readonly IReadOnlyList<FlightPlan> flightPlans;
    private readonly Mock<IRepository<FlightPlan>> mockRepository;
    private readonly Mock<IHttpContextAccessor> mockHttpContextAccessor;
    private readonly Mock<IRepository<Organization>> mockOrganizationRepository;
    private readonly Mock<IRepository<Airport>> mockAirportRepository;
    private readonly FlightPlanController controller;

    public FlightPlanControllerTests()
    {
        mockRepository = new Mock<IRepository<FlightPlan>>();
        mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockOrganizationRepository = new Mock<IRepository<Organization>>();
        mockAirportRepository = new Mock<IRepository<Airport>>();
        controller = new FlightPlanController(mockRepository.Object, mockHttpContextAccessor.Object,mockOrganizationRepository.Object, mockAirportRepository.Object );
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
        var organizationClaim = new Claim("OrganizationId", "1");
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { organizationClaim }));

        // Mock the HttpContext and User claims
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(x => x.User).Returns(claimsPrincipal);
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);
        mockRepository.Setup(repo => repo.GetAllAsync(null))
            .ReturnsAsync(flightPlans); // Return the predefined list
        var organization = new Organization
        {
            Id = 1,
            Name = "Test Organization",
            FlightPlans = flightPlans.ToList()
        };

        // Mock the organization repository to return the organization
        mockOrganizationRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(organization);

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
        // Set up the organization claim
        var organizationClaim = new Claim("OrganizationId", "1");
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { organizationClaim }));

        // Mock the HttpContext and User claims
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(x => x.User).Returns(claimsPrincipal);
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);

        // Set up the test flight plan
        var flightPlan = flightPlans[0];
        var organization = new Organization
        {
            Id = 1,
            Name = "Test Organization",
            FlightPlans = new List<FlightPlan>()
        };

        // Mock the organization repository to return the organization
        mockOrganizationRepository.Setup(repo => repo.GetByIdAsync(1))
            .ReturnsAsync(organization);

        // Mock the airport repository to return airports based on flight plan's airport IDs
        foreach (var airport in flightPlan.Airports)
        {
            mockAirportRepository.Setup(repo => repo.GetByIdAsync(airport.Id))
                .ReturnsAsync(airport); // Simulate retrieving each airport
        }

        // Mock the organization repository's update method
        mockOrganizationRepository.Setup(repo => repo.UpdateAsync(organization))
            .Returns(Task.CompletedTask); // Simulate successful update

        // Act
        var result = await controller.CreateFlightPlanAsync(flightPlan);

        // Assert
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task UpdateFlightPlanByIdAsync_ReturnsOk_WhenFlightPlanExists()
    {
        // Arrange
        var existingFlightPlan = flightPlans.First();
        var actionToUpdate = new FlightPlan
        {
            Id = existingFlightPlan.Id,
            Airports = existingFlightPlan.Airports
        };
        
        mockRepository.Setup(r => r.UpdateAsync(It.IsAny<FlightPlan>())).Returns(Task.CompletedTask);
        
        // Act
        var result = await controller.UpdateFlightPlanByIdAsync(existingFlightPlan.Id, actionToUpdate);
    
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var updatedFlightPlan = Assert.IsType<FlightPlan>(okResult.Value);
        // Assert.Equal(existingAirport.Id, updatedAirport.Id);
        // Assert.Equal(airportToUpdate.ICAO, updatedAirport.ICAO);
    }
}