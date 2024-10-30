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
            // Step 1: Start with identifiers of canceled Notams
            var excludedReferenceIds = await _dbSet
                .Where(n => n.Type == NotamType.Cancellation)
                .Select(n => n.ReferenceIdentifier)
                .ToListAsync();

            // Step 2: Recursively expand the exclusion list for any Notams that have a ReferenceIdentifier in excludedReferenceIds
            bool newReferencesFound;
            do
            {
                // Find Notams that reference any identifier in the current exclusion list
                var newReferences = await _dbSet
                    .Where(n => n.ReferenceIdentifier != null // Only Notams with a reference
                                && excludedReferenceIds.Contains(n.ReferenceIdentifier) // Referencing an excluded Identifier
                                && !excludedReferenceIds.Contains(n.Identifier)) // Not already in the excluded list
                    .Select(n => n.Identifier)
                    .ToListAsync();

                newReferencesFound = newReferences.Any();
                excludedReferenceIds.AddRange(newReferences); // Add any newly found references

            } while (newReferencesFound);

            var newrefs = await _dbSet.Where(n => n.ReferenceIdentifier == null&&!excludedReferenceIds.Contains(n.Identifier)).Select(n => n.Identifier).ToListAsync();
            excludedReferenceIds.AddRange(newrefs);
            // Step 3: Exclude any Notam that is canceled or references a canceled identifier
            return await _dbSet
                .Include(n => n.Coordinates)
                .Where(n => n.Type != NotamType.Cancellation // Exclude Notams of type Cancellation
                            && (!excludedReferenceIds.Contains(n.Identifier))) // Exclude in the reference chain
                .GroupJoin(_context.NotamActions.Where(na => na.OrganizationId == organizationId),
                    notam => notam.Id,
                    action => action.NotamId,
                    (notam, actions) => new { Notam = notam, Actions = actions })
                .SelectMany(
                    x => x.Actions.DefaultIfEmpty(),
                    (x, action) => new { x.Notam, Action = action })
                .Where(x => x.Action == null) // Only unhandled Notams
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
