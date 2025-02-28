using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using System.Linq.Expressions;

namespace NotamManagement.Core.Repository
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly NotamManagementContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(NotamManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IReadOnlyList<User> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task RemoveAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(User entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IReadOnlyList<User> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
