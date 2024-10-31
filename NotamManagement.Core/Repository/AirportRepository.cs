using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using System.Linq.Expressions;

namespace NotamManagement.Core.Repository
{
    public class AirportRepository : IRepository<Airport>
    {
        private readonly NotamManagementContext _context;
        private readonly DbSet<Airport> _dbSet;

        public AirportRepository(NotamManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<Airport>();
        }

        public async Task AddAsync(Airport entity)
        {
            await _dbSet.AddAsync(entity);
            _context.SaveChanges();
        }


        public Task<IReadOnlyList<Airport>> GetAllUnhandledAsync(int organizationId)
        {
            throw new NotImplementedException();
        }


        public Task AddRangeAsync(IReadOnlyList<Airport> entities)

        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Airport> GetAllAsAsyncEnumerable(int? organizationId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Airport>> FindAsync(Expression<Func<Airport, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<Airport>> GetAllAsync(int? organizationId = null)
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Airport?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);

        }

        public async Task RemoveAsync(int id)
        {
            var airport = await GetByIdAsync(id);
            if (airport != null)
            {
                _dbSet.Remove(airport);
                _context.SaveChanges();
            }

        }

        public async Task RemoveAsync(Airport entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveRangeAsync(IReadOnlyList<Airport> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Airport entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
