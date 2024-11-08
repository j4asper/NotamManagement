using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Core.RepositoryTests;

public class NotamActionRepositoryTests
{
    private readonly IReadOnlyList<NotamAction> notamActions;
    private readonly NotamManagementContext context;

    public NotamActionRepositoryTests()
    {
        context = DatabaseHelper.GetInMemoryDbContext();
        notamActions = NotamActionHelper.GetTestData();
    }
    
    [Fact]
    public async Task AddNotamAction_ShouldAddNotamAction()
    {
        var repository = new NotamActionRepository(context);
        
        // Arrange
        var notam = notamActions[0];

        // Act
        await repository.AddAsync(notam);

        // Assert
        var result = await repository.FindAsync(x => x.Id == notam.Id);
        
        Assert.NotNull(result.First());
        Assert.Equal(notam.Note, result.First().Note);
    }

    [Fact]
    public async Task GetNotamAction_ShouldReturnNull_WhenNotFound()
    {
        var repository = new NotamActionRepository(context);
        
        // Act
        var result = await repository.FindAsync(x => x.Id == int.MaxValue); // ID that doesn't exist

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetNotamActionById_ShouldReturnNotamAction()
    {
        var repository = new NotamActionRepository(context);
        
        // Arrange
        var notamAction = notamActions[0];
        
        await repository.AddAsync(notamAction);
        
        // Act
        var result = await repository.GetByIdAsync(notamAction.Id); // ID that doesn't exist

        // Assert
        Assert.Equal(notamAction.Id, result.Id);
    }

    [Fact]
    public async Task GetAllNotamActions_ShouldReturnAllNotamActions()
    {
        var repository = new NotamActionRepository(context);
        
        // Arrange
        await repository.AddRangeAsync(notamActions);
        
        // Act
        var result = await repository.GetAllAsync(null); // ID that doesn't exist

        // Assert
        Assert.Equal(notamActions.Count, result.Count);
    }
    
    [Fact]
    public async Task RemoveAsync_ShouldRemoveNotamAction()
    {
        var repository = new NotamActionRepository(context);
        
        // Arrange
        var notamAction = notamActions[0];
        await repository.AddAsync(notamAction);

        // Act
        await repository.RemoveAsync(notamAction.Id);

        // Assert
        var result = await repository.FindAsync(x => x.Id == notamAction.Id);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task RemoveByObjectAsync_ShouldRemoveNotamAction()
    {
        var repository = new NotamActionRepository(context);
        
        // Arrange
        var notamAction = notamActions[0];
        await repository.AddAsync(notamAction);

        // Act
        await repository.RemoveAsync(notamAction);

        // Assert
        var result = await repository.FindAsync(x => x.Id == notamAction.Id);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateNotamAction()
    {
        var repository = new NotamActionRepository(context);
        
        // Arrange
        var notamAction = notamActions[0];
        await repository.AddAsync(notamAction);
        notamAction.Note = "Updated Note";

        // Act
        await repository.UpdateAsync(notamAction);

        // Assert
        var result = await repository.GetByIdAsync(notamAction.Id);
        Assert.Equal("Updated Note", result.Note);
    }
    
    [Fact]
    public async Task RemoveRangeAsync_ShouldRemoveNotamActions()
    {
        var repository = new NotamActionRepository(context);
        
        // Arrange
        await repository.AddAsync(notamActions.First());

        // Act
        await repository.RemoveRangeAsync([notamActions.First()]);

        // Assert
        var result = await repository.FindAsync(x => x.Id == notamActions.First().Id);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetAllUnhandledAsAsyncEnumerable_ShouldThrowNotImplementedException()
    {
        var repository = new NotamActionRepository(context);
        
        Assert.Throws<NotImplementedException>(() => repository.GetAllUnhandledAsAsyncEnumerable(0));
    }
    
    [Fact]
    public async Task GetAllUnhandledAsync_ShouldThrowNotImplementedException()
    {
        var repository = new NotamActionRepository(context);
        
        await Assert.ThrowsAsync<NotImplementedException>(() => repository.GetAllUnhandledAsync(0));
    }
    
    [Fact]
    public async Task GetAllAsAsyncEnumerable_ShouldThrowNotImplementedException()
    {
        var repository = new NotamActionRepository(context);
        
        Assert.Throws<NotImplementedException>(() => repository.GetAllAsAsyncEnumerable(0));
    }
}