﻿using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using System.Linq.Expressions;

namespace NotamManagement.Core.Repository
{
    public class NotamActionRepository : IRepository<NotamAction>
    {
        private readonly NotamManagementContext _context;
        private readonly DbSet<NotamAction> _dbSet;

        public NotamActionRepository(NotamManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<NotamAction>();
        }

        public async Task AddAsync(NotamAction entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<NotamAction> GetAllUnhandledAsAsyncEnumerable(int organizationId)
        {
            throw new NotImplementedException();
        }


        public Task<IReadOnlyList<NotamAction>> GetAllUnhandledAsync(int organizationId)
        {
            throw new NotImplementedException();
        }


        public async Task AddRangeAsync(IReadOnlyList<NotamAction> entities)

        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<NotamAction> GetAllAsAsyncEnumerable(int? organizationId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<NotamAction>> FindAsync(Expression<Func<NotamAction, bool>> predicate)
        {
            return await _dbSet.Include(x=>x.Notam).ThenInclude(x=>x.Coordinates).Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<NotamAction>> GetAllAsync(int? organizationId)
        {
            if(organizationId.HasValue)
            {
                return await _dbSet.Include(x=>x.Notam).ThenInclude(x=>x.Coordinates).Where(x => x.OrganizationId == organizationId).ToListAsync();
            }
            return await _dbSet.ToListAsync();
            
        }

        public async Task<NotamAction?> GetByIdAsync(int id)
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

        public async Task RemoveAsync(NotamAction entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IReadOnlyList<NotamAction> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NotamAction entity)
        {
            var trackedEntity = await _dbSet.FindAsync(entity.Id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }
            await _context.SaveChangesAsync();
        }
    }
}
