using SportSparkCoreLibrary.Entities;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDTO> Login(User user);

        Task<UserDTO> GetByIdAsync(int id);
    }
}
