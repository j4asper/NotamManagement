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

        public Task AddRangeAsync(IEnumerable<Airport> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Airport>> FindAsync(Expression<Func<Airport, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Airport>> GetAllAsync()
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

        public Task RemoveAsync(Airport entity)
        {
            _dbSet.Remove(entity);
           _context.SaveChanges();
            return Task.CompletedTask;
        }
    

        public Task RemoveRangeAsync(IEnumerable<Airport> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Airport entity)
        {
            throw new NotImplementedException();
        }
    }


}
