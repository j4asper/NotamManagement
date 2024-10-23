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
    public class FlightPlanRepository : IRepository<FlightPlan>
    {
        private readonly NotamManagementContext _context;
        private readonly DbSet<FlightPlan> _dbSet;

        public FlightPlanRepository(NotamManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<FlightPlan>();
        }

        public Task AddAsync(FlightPlan entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<FlightPlan> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FlightPlan>> FindAsync(Expression<Func<FlightPlan, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FlightPlan>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FlightPlan?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(FlightPlan entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<FlightPlan> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(FlightPlan entity)
        {
            throw new NotImplementedException();
        }
    }
}
