using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotamManagement.Core.Repository
{
    public interface IRepository<T> where T : class
    {
        // Basic CRUD operations

        // Get by Id
        Task<T?> GetByIdAsync(int id);

        // Get all entities
        Task<IEnumerable<T>> GetAllAsync();

        // Find entities based on criteria
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Add a new entity
        Task AddAsync(T entity);

        Task<IReadOnlyList<T>> GetAllUnhandledAsync(int organizationId);

        // Add multiple entities
        Task AddRangeAsync(IEnumerable<T> entities);

        // Update an existing entity
        Task UpdateAsync(T entity);

        // Remove an entity by id
        Task RemoveAsync(int id);

        // Remove an entity
        Task RemoveAsync(T entity);

        // Remove multiple entities
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
