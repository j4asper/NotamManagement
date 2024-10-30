using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using System.Linq.Expressions;

namespace NotamManagement.Core.Repository
{
    public class NotamRepository : IRepository<Notam>
    {
        private readonly NotamManagementContext _context;
        private readonly DbSet<Notam> _dbSet;
        //private readonly DbSet<Coordinates> _coordinatesSet;

        public NotamRepository(NotamManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<Notam>();
            //  _coordinatesSet = _context.Set<Coordinates>();
        }

        public async Task<Notam?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(n => n.Coordinates).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Notam>> GetAllAsync()
        {
            return await _dbSet.Include(n => n.Coordinates).ToListAsync();
        }

        public async Task<IReadOnlyList<Notam>> FindAsync(Expression<Func<Notam, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Notam entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IReadOnlyList<Notam> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notam entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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

        public async Task RemoveAsync(Notam entity)
        {
            
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IReadOnlyList<Notam> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

    }
}
