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

        public Task AddRangeAsync(IEnumerable<Organization> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Organization>> FindAsync(Expression<Func<Organization, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Organization>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Organization?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Organization entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<Organization> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Organization entity)
            {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
