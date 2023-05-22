using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Services.Base;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface IUserService : IBaseService<UserDTO>
    {
        UserDTO Login(User user);

        Task<User> UserValid(string emailOrUserName, string password);

        Task AddAsFriendAsync(int senderId, int receiverId);

        Task UpdateProfilePictureAsync(int userId, int newDocumentId);
    }
}
