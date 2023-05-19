using SportSparkCoreLibrary.Interfaces.Services.Base;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface IFriendshipService : IBaseService<FriendshipDTO>
    {
        Task<List<FriendshipDTO>> GetReceivedFriendshipsAsync(int id);
    }
}
