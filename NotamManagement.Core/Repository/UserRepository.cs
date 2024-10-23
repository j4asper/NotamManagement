using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotamManagement.Core.Repository
{
    public class UserRepository : IRepository<User>
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

        public Task AddRangeAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
           return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
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

        public async Task RemoveRangeAsync(IEnumerable<User> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
