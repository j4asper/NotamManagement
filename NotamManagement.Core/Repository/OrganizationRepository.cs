using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using System.Linq.Expressions;

namespace NotamManagement.Core.Repository
{
    public class OrganizationRepository : IRepository<Organization>
    {
        private readonly NotamManagementContext _context;
        private readonly DbSet<Organization> _dbSet;

        public OrganizationRepository(NotamManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<Organization>();
        }


        public async Task AddAsync(Organization entity)
        {
            await _dbSet.AddAsync(entity);
            _context.SaveChanges();
        }

        public IAsyncEnumerable<Organization> GetAllUnhandledAsAsyncEnumerable(int organizationId)
        {
            throw new NotImplementedException();
        }


        public Task<IReadOnlyList<Organization>> GetAllUnhandledAsync(int organizationId)
        {
            throw new NotImplementedException();
        }


        public async Task AddRangeAsync(IReadOnlyList<Organization> entities)

        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<Organization> GetAllAsAsyncEnumerable(int? organizationId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Organization>> FindAsync(Expression<Func<Organization, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<Organization>> GetAllAsync(int? organizationId = null)
        {
          return await _dbSet.ToListAsync();
        }

        public async Task<Organization?> GetByIdAsync(int id)
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

        public async Task RemoveAsync(Organization entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IReadOnlyList<Organization> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Organization entity)
            {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
