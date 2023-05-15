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
        public async Task AddFriendship(Friendship friendship)
        {
            await _dbContext.AddAsync(friendship);
            await _dbContext.SaveChangesAsync();
        }
    }
}
