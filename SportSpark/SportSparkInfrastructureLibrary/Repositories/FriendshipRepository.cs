using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Enums;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkInfrastructureLibrary.Database;

namespace SportSparkInfrastructureLibrary.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly SportSparkDBContext _dbContext;
        public FriendshipRepository(SportSparkDBContext context)
        {
            _dbContext = context;
        }
        public async Task AddFriendshipAsync(Friendship friendship)
        {
            await _dbContext.AddAsync(friendship);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckFriendshipExistsAsync(int id, int id_2)
        {
            Friendship friendship = await _dbContext.Friendships.FirstOrDefaultAsync(x => (x.SenderId == id && x.ReceiverId == id_2) || (x.ReceiverId == id && x.SenderId == id_2));

            return friendship != null;
        }

        public async Task<List<Friendship>> GetReceivedFriendshipsAsync(int id)
        {
            return await _dbContext.Friendships
                .Include(x => x.Sender)
                    .ThenInclude(_ => _.Events)
                .Include(x => x.Sender)
                    .ThenInclude(_ => _.ProfileImage)
                .Where(x => x.ReceiverId == id && x.Status == FriendshipStatus.Requested).ToListAsync();
        }

        public async Task<Friendship> UpdateAsync(Friendship entity)
        {
            _ = entity
                ?? throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

            Friendship existing = await _dbContext.Set<Friendship>().FindAsync(entity.Id);

            if (existing != null)
            {
                _dbContext.Entry(existing).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Entity does not exist");
            }

            return existing;
        }
    }
}
