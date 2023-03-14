using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkInfrastructureLibrary.Database;

namespace SportSparkInfrastructureLibrary.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly SportSparkDBContext _context;
        private DbSet<T> _entities;

        public BaseRepository(SportSparkDBContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public Task<List<T>> GetAllAsync(int id)
        {
            return _entities.AsNoTracking().ToListAsync();
        }

        public Task<T> GetByIdAsync(int id)
        {
            return _entities.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
