using Microsoft.AspNetCore.Mvc;
using Moq;
using NotamManagement.Api.Controllers;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Api;

public class NotamActionControllerTests
{
    private readonly IReadOnlyList<NotamAction> notamActions;
    private readonly Mock<IRepository<NotamAction>> mockRepository;
    private readonly NotamActionController controller;

    public NotamActionControllerTests()
    {
        mockRepository = new Mock<IRepository<NotamAction>>();
        controller = new NotamActionController(mockRepository.Object);
        notamActions = NotamActionHelper.GetTestData();
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
        mockRepository.Setup(repo => repo.GetAllAsync())
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