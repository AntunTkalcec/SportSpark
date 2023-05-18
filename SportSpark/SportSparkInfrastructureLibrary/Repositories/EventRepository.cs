using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Database;
using SportSparkInfrastructureLibrary.Repositories.Base;

namespace SportSparkInfrastructureLibrary.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(SportSparkDBContext context) : base(context)
        {
        }

        public async Task<List<int>> GetEventsByLocation(LatLongWrapperDTO wrapper, double radius)
        {
            //must call ToListAsync() to make query composable
            var events = await _context.Events.FromSqlInterpolated(
                @$"EXEC dbo.GetEventsByLocation @latitude = {wrapper.Latitude}, 
                @longitude = {wrapper.Longitude}, @radius = {radius}").ToListAsync();

            return events.Select(_ => _.Id).ToList();
        }
    }
}
