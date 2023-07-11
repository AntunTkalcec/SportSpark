using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkWeb.Actions;

public interface IUserStateStoreActions
{
    Task ChangeState(UserDTO user);
}
