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


        public async Task<IReadOnlyList<Notam>> GetAllUnhandledAsync(int organizationId)
        {
            // Step 1: Initialize a list of identifiers to exclude, starting with all canceled Notams
            var excludedReferenceIds = await _dbSet
                .Where(n => n.Type == NotamType.Cancellation)
                .Select(n => n.Identifier)
                .ToListAsync();

            // Step 2: Find all Notams that reference any excluded identifier (recursive chain)
            bool newReferencesFound;
            do
            {
                // Find new references that should also be excluded
                var newReferences = await _dbSet
                    .Where(n => n.ReferenceIdentifier != null
                                && excludedReferenceIds.Contains(n.ReferenceIdentifier)
                                && !excludedReferenceIds.Contains(n.Identifier))
                    .Select(n => n.Identifier)
                    .ToListAsync();

                newReferencesFound = newReferences.Any();
                excludedReferenceIds.AddRange(newReferences);

            } while (newReferencesFound);

            // Step 3: Query to get all unhandled Notams, excluding those in the canceled reference chain
            return await _dbSet
                .Include(n => n.Coordinates)
                .Where(n => n.Type != NotamType.Cancellation
                            && (n.ReferenceIdentifier == null || !excludedReferenceIds.Contains(n.ReferenceIdentifier)))
                .GroupJoin(_context.NotamActions.Where(na => na.OrganizationId == organizationId),
                    notam => notam.Id,
                    action => action.NotamId,
                    (notam, actions) => new { Notam = notam, Actions = actions })
                .SelectMany(
                    x => x.Actions.DefaultIfEmpty(),
                    (x, action) => new { x.Notam, Action = action })
                .Where(x => x.Action == null)
                .Select(x => x.Notam)
                .ToListAsync();
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
