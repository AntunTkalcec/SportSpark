using SportSparkCoreLibrary.Entities;

namespace SportSparkCoreLibrary.Interfaces.Repositories
{
    public interface IFriendshipRepository
    {
        Task AddFriendship(Friendship friendship);
    }
}
