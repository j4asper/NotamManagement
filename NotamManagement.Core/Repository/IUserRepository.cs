using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotamManagement.Core.Repository
{
    public interface IUserRepository<T> where T : class
    {
        // Basic CRUD operations

        // Get by Id
        Task<T?> GetByIdAsync(string id);

        // Get all entities
        Task<IEnumerable<T>> GetAllAsync();

        // Find entities based on criteria
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Add a new entity
        Task AddAsync(T entity);

        // Add multiple entities
        Task AddRangeAsync(IEnumerable<T> entities);

        // Update an existing entity
        Task UpdateAsync(T entity);

        // Remove an entity by id
        Task RemoveAsync(string id);

        // Remove an entity
        Task RemoveAsync(T entity);

        // Remove multiple entities
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
