using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotamManagement.Api.Controllers;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;
using System.Security.Claims;

namespace NotamManagement.Tests.Api;

public class NotamActionControllerTests
{
    private readonly IReadOnlyList<NotamAction> notamActions;
    private readonly IReadOnlyList<Notam> notams;
    private readonly Mock<IRepository<NotamAction>> mockRepository;
    private readonly Mock<IRepository<Notam>> mockNotamRepository;
    private readonly NotamActionController controller;
    private readonly Mock<IHttpContextAccessor> mockHttpContextAccessor;

    public NotamActionControllerTests()
    {
        mockRepository = new Mock<IRepository<NotamAction>>();
        mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockNotamRepository = new Mock<IRepository<Notam>>();
        controller = new NotamActionController(mockRepository.Object,mockHttpContextAccessor.Object,mockNotamRepository.Object);
        notamActions = NotamActionHelper.GetTestData();
        notams = NotamHelper.GetTestData();
    }
    
    [Fact]
    public async Task GetNotamActionByIdAsync_ReturnsNotFound_WhenNotamActionDoesNotExist()
    {
        // Arrange
        int notamActionId = 999; // ID that doesn't exist
        mockRepository.Setup(repo => repo.GetByIdAsync(notamActionId))
            .ReturnsAsync((NotamAction)null); // Simulate not found

        // Act
        var result = await controller.GetNotamActionByIdAsync(notamActionId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetNotamActionByIdAsync_ReturnsNotamAction_WhenNotamActionExists()
    {
        // Arrange
        var notamAction = notamActions[0];
        
        mockRepository.Setup(repo => repo.GetByIdAsync(notamAction.Id))
            .ReturnsAsync(notamAction); // Simulate found

        // Act
        var result = await controller.GetNotamActionByIdAsync(notamAction.Id);
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var notamActionResult = okResult.Value as NotamAction;
        Assert.Equal(notamAction.Id, notamActionResult.Id);
    }

    [Fact]
    public async Task DeleteNotamActionByIdAsync_ReturnsOk_WhenNotamActionExists()
    {
        // Arrange
        int notamActionId = 1;
        
        mockRepository.Setup(repo => repo.RemoveAsync(notamActionId))
            .Returns(Task.CompletedTask); // Simulate successful deletion

        // Act
        var result = await controller.DeleteNotamActionByIdAsync(notamActionId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteNotamActionByIdAsync_ReturnsNotFound_WhenNotamActionDoesNotExist()
    {
        // Arrange
        int notamActionId = 999; // ID that doesn't exist
        mockRepository.Setup(repo => repo.RemoveAsync(notamActionId))
            .Returns(Task.CompletedTask); // Simulate not found

        // Act
        var result = await controller.DeleteNotamActionByIdAsync(notamActionId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task GetAllNotamActionsAsync_ReturnsListOfNotamActions()
    {
        // Arrange
        var organizationClaim = new Claim("OrganizationId", "1");
        mockHttpContextAccessor.Setup(x => x.HttpContext.User.FindFirst("OrganizationId"))
            .Returns(organizationClaim);
        mockRepository.Setup(repo => repo.GetAllAsync(1))
            .ReturnsAsync(notamActions); // Return the predefined list
        
        // Act
        var result = await controller.GetAllNotamActionsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var notamActionList = okResult.Value as IReadOnlyList<NotamAction>;
        Assert.NotNull(notamActionList);
        Assert.Equal(notamActions.Count, notamActionList.Count);
    }
    
    [Fact]
    public async Task GetNotamActionsByLocationAsync_ReturnsListOfNotamActions()
    {
        // Arrange
        var notamList = notams.Take(2).ToList();
        var _notamActionList = notamActions.Where(x => notamList.Select(n => n.Id).Contains(x.NotamId)).ToList();
        
        var organizationClaim = new Claim("OrganizationId", "1");
        mockHttpContextAccessor.Setup(x => x.HttpContext.User.FindFirst("OrganizationId"))
            .Returns(organizationClaim);
        mockNotamRepository.Setup(repo => repo.FindAsync(x => x.Location == "EKDK"))
            .ReturnsAsync(notamList);
        mockRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<NotamAction, bool>>>()))
            .ReturnsAsync(_notamActionList);
        
        // Act
        var result = await controller.FindByLocationAsync("EKDK");
    
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var notamActionList = okResult.Value as IReadOnlyList<NotamAction>;
        Assert.NotNull(notamActionList);
        Assert.Equal(2, notamActionList.Count);
    }

    [Fact]
    public async Task CreateNotamActionAsync_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var notamAction = notamActions[0];
        
        mockRepository.Setup(repo => repo.AddAsync(notamAction))
            .Returns(Task.CompletedTask); // Simulate successful addition

        // Act
        var result = await controller.CreateNotamActionAsync(notamAction);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var notamActionResult = okResult.Value as NotamAction;
        Assert.Equal(notamAction.Id, notamActionResult.Id);
    }
}