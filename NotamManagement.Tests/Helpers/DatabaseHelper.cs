using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Data;

namespace NotamManagement.Tests.Helpers;

public static class DatabaseHelper
{
    public static NotamManagementContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<NotamManagementContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new NotamManagementContext(options);
    }
}