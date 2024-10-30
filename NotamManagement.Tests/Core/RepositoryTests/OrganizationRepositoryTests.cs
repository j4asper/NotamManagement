using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using NotamManagement.Core.Repository;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Core.RepositoryTests;

public class OrganizationRepositoryTests
{
    private readonly IReadOnlyList<Organization> organizations;
    private readonly NotamManagementContext context;

    public OrganizationRepositoryTests()
    {
        context = DatabaseHelper.GetInMemoryDbContext();
        organizations = OrganizationHelper.GetTestData();
    }
    
    [Fact]
    public async Task AddOrganization_ShouldAddOrganization()
    {
        var repository = new OrganizationRepository(context);
        
        // Arrange
        var organization = organizations[0];

        // Act
        await repository.AddAsync(organization);

        // Assert
        var result = await repository.FindAsync(x => x.Id == organization.Id);
        
        Assert.NotNull(result.First());
        Assert.Equal(organization.Name, result.First().Name);
    }

    [Fact]
    public async Task GetOrganization_ShouldReturnNull_WhenNotFound()
    {
        var repository = new OrganizationRepository(context);
        
        // Act
        var result = await repository.FindAsync(x => x.Id == int.MaxValue); // ID that doesn't exist

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetOrganizationById_ShouldReturnOrganization()
    {
        var repository = new OrganizationRepository(context);
        
        // Arrange
        var organization = organizations[0];
        
        await repository.AddAsync(organization);
        
        // Act
        var result = await repository.GetByIdAsync(organization.Id); // ID that doesn't exist

        // Assert
        Assert.Equal(organization.Id, result.Id);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveOrganization()
    {
        var repository = new OrganizationRepository(context);
        
        // Arrange
        var organization = organizations[0];
        await repository.AddAsync(organization);

        // Act
        await repository.RemoveAsync(organization.Id);

        // Assert
        var result = await repository.FindAsync(x => x.Id == organization.Id);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task RemoveByObjectAsync_ShouldRemoveOrganization()
    {
        var repository = new OrganizationRepository(context);
        
        // Arrange
        var organization = organizations[0];
        await repository.AddAsync(organization);

        // Act
        await repository.RemoveAsync(organization);

        // Assert
        var result = await repository.FindAsync(x => x.Id == organization.Id);
        Assert.Empty(result);
    }


    [Fact]
    public async Task UpdateAsync_ShouldUpdateOrganization()
    {
        var repository = new OrganizationRepository(context);
        
        // Arrange
        var organization = organizations[0];
        await repository.AddAsync(organization);
        organization.Name = "HOP";

        // Act
        await repository.UpdateAsync(organization);

        // Assert
        var result = await repository.GetByIdAsync(organization.Id);
        Assert.Equal("HOP", result.Name);
    }
}