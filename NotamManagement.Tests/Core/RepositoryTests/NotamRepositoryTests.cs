using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Core.RepositoryTests;

public class NotamRepositoryTests
{
    private readonly IReadOnlyList<Notam> notams;
    private readonly NotamManagementContext context;

    public NotamRepositoryTests()
    {
        context = DatabaseHelper.GetInMemoryDbContext();
        notams = NotamHelper.GetTestData();
    }
    
    [Fact]
    public async Task AddNotam_ShouldAddNotam()
    {
        var repository = new NotamRepository(context);
        
        // Arrange
        var notam = notams[0];

        // Act
        await repository.AddAsync(notam);

        // Assert
        var result = await repository.FindAsync(x => x.Id == notam.Id);
        
        Assert.NotNull(result.First());
        Assert.Equal(notam.NotamText, result.First().NotamText);
    }

    [Fact]
    public async Task GetNotam_ShouldReturnNull_WhenNotFound()
    {
        var repository = new NotamRepository(context);
        
        // Act
        var result = await repository.FindAsync(x => x.Id == 999999999); // ID that doesn't exist

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveNotam()
    {
        var repository = new NotamRepository(context);
        
        // Arrange
        var notam = notams[0];
        await repository.AddAsync(notam);

        // Act
        await repository.RemoveAsync(notam.Id);

        // Assert
        var result = await repository.FindAsync(x => x.Id == notam.Id);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateNotam()
    {
        var repository = new NotamRepository(context);
        
        // Arrange
        var notam = notams[0];
        await repository.AddAsync(notam);
        notam.NotamText = "Updated Notam Text";

        // Act
        await repository.UpdateAsync(notam);

        // Assert
        var result = await repository.GetByIdAsync(notam.Id);
        Assert.Equal("Updated Notam Text", result.NotamText);
    }
}