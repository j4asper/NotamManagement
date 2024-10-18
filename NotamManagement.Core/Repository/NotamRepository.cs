using Microsoft.EntityFrameworkCore;
using NotamManagement.Core.Data;
using NotamManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Notam>> GetAllAsync()
        {

            return await _dbSet.Include(n => n.Coordinates).ToListAsync();
        }

        public async Task<IEnumerable<Notam>> FindAsync(Expression<Func<Notam, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Notam entity)
        {
            await _dbSet.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<Notam> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(Notam entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }

        }

        public async Task RemoveAsync(Notam entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveRangeAsync(IEnumerable<Notam> entities)
        {
            _dbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }

        public Notam GetById(int id)
        {
            return _dbSet.Find(id) ?? throw new InvalidOperationException("Entity not found");
        }

        public IEnumerable<Notam> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(Notam entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(Notam entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Notam entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
