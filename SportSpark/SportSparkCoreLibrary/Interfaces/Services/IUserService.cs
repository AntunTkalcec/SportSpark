using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Services.Base;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkCoreLibrary.Interfaces.Services
{
    public interface IUserService : IBaseService<UserDTO>
    {
        Task<UserDTO> Login(User user);
    }
}
