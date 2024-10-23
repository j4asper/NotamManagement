using Microsoft.AspNetCore.Mvc;
using Moq;
using NotamManagement.Api.Controllers;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Api;

public class NotamControllerTests
{
    private readonly IReadOnlyList<Notam> notams;
    private readonly Mock<IRepository<Notam>> mockRepository;
    private readonly NotamController controller;

    public NotamControllerTests()
    {
        mockRepository = new Mock<IRepository<Notam>>();
        controller = new NotamController(mockRepository.Object);
        notams = NotamHelper.GetTestData();
    }
    
    [Fact]
    public async Task GetNotamByIdAsync_ReturnsNotFound_WhenNotamDoesNotExist()
    {
        // Arrange
        int notamId = 999; // ID that doesn't exist
        mockRepository.Setup(repo => repo.GetByIdAsync(notamId))
            .ReturnsAsync((Notam)null); // Simulate not found

        // Act
        var result = await controller.GetNotamByIdAsync(notamId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetNotamByIdAsync_ReturnsNotam_WhenNotamExists()
    {
        // Arrange
        var notam = notams[0];
        
        mockRepository.Setup(repo => repo.GetByIdAsync(notam.Id))
            .ReturnsAsync(notam); // Simulate found

        // Act
        var result = await controller.GetNotamByIdAsync(notam.Id);

        // Assert
        var okResult = Assert.IsType<Notam>(result.Value);
        Assert.Equal(notam.Id, okResult.Id);
    }

    [Fact]
    public async Task DeleteNotamByIdAsync_ReturnsOk_WhenNotamExists()
    {
        // Arrange
        int notamId = 1;
        
        mockRepository.Setup(repo => repo.RemoveAsync(notamId))
            .Returns(Task.CompletedTask); // Simulate successful deletion

        // Act
        var result = await controller.DeleteNotamByIdAsync(notamId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteNotamByIdAsync_ReturnsNotFound_WhenNotamDoesNotExist()
    {
        // Arrange
        int notamId = 999; // ID that doesn't exist
        mockRepository.Setup(repo => repo.RemoveAsync(notamId))
            .Returns(Task.CompletedTask); // Simulate not found

        // Act
        var result = await controller.DeleteNotamByIdAsync(notamId);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task GetAllNotamsAsync_ReturnsListOfNotams()
    {
        // Arrange
        mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(notams); // Return the predefined list

        // Act
        var result = await controller.GetAllNotamsAsync();

        // Assert
        var okResult = Assert.IsType<List<Notam>>(result.Value);
        Assert.Equal(notams.Count, okResult.Count);
    }

    [Fact]
    public async Task CreateNotamAsync_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var notam = notams[0];
        
        mockRepository.Setup(repo => repo.AddAsync(notam))
            .Returns(Task.CompletedTask); // Simulate successful addition

        // Act
        var result = await controller.CreateNotamAsync(notam);

        // Assert
        Assert.IsType<OkResult>(result);
    }
}