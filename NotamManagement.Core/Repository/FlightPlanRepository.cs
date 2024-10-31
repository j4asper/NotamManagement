﻿using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using System.Linq.Expressions;

namespace NotamManagement.Core.Repository
{
    public class FlightPlanRepository : IRepository<FlightPlan>
    {
        private readonly NotamManagementContext _context;
        private readonly DbSet<FlightPlan> _dbSet;

        public FlightPlanRepository(NotamManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<FlightPlan>();
        }

        public async Task AddAsync(FlightPlan entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public Task<IReadOnlyList<FlightPlan>> GetAllUnhandledAsync(int organizationId)
        {
            throw new NotImplementedException();
        }


        public async Task AddRangeAsync(IReadOnlyList<FlightPlan> entities)

        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<FlightPlan> GetAllAsAsyncEnumerable(int? organizationId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<FlightPlan>> FindAsync(Expression<Func<FlightPlan, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<FlightPlan>> GetAllAsync(int? organizationId = null)
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<FlightPlan?> GetByIdAsync(int id)
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

        public async Task RemoveAsync(FlightPlan entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IReadOnlyList<FlightPlan> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FlightPlan entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
