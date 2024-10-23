using NotamManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotamManagement.Core.Repository
{
    public class NotamActionRepository : IRepository<NotamAction>
    {
        public Task AddAsync(NotamAction entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<NotamAction> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NotamAction>> FindAsync(Expression<Func<NotamAction, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NotamAction>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<NotamAction?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(NotamAction entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<NotamAction> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(NotamAction entity)
        {
            throw new NotImplementedException();
        }
    }
}
