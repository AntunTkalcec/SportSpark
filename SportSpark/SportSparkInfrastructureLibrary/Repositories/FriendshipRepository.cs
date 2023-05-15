using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;
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
    }
}
