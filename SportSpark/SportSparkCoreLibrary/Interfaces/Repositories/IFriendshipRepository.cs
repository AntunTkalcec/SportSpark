using SportSparkCoreLibrary.Entities;

namespace SportSparkCoreLibrary.Interfaces.Repositories
{
    public interface IFriendshipRepository
    {
        Task AddFriendshipAsync(Friendship friendship);
        Task<bool> CheckFriendshipExistsAsync(int id, int id_2);

        Task<List<Friendship>> GetReceivedFriendshipsAsync(int id);
        Task<Friendship> UpdateAsync(Friendship friendship);
    }
}
