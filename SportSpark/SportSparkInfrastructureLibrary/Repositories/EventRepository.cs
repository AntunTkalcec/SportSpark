using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkInfrastructureLibrary.Database;
using SportSparkInfrastructureLibrary.Repositories.Base;

namespace SportSparkInfrastructureLibrary.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private readonly SportSparkDBContext _context;

        public EventRepository(SportSparkDBContext context) : base(context)
        {
            _context = context;
        }

        public Task<Event> GetByIdDetailedAsync(int id)
        {
            return _context.Events
                .Include(x => x.User)
                .Include(x => x.EventType)
                .Include(x => x.RepeatType)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
