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

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
