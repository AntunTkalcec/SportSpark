using SportSparkCoreSharedLibrary.DTOs;
using SportSparkWeb.Actions;
using SportSparkWeb.States;
using System.Reactive.Subjects;

namespace SportSparkWeb.StateStores;

public class UserStateStore : StateStoreBase<UserState>, IUserStateStoreActions
{
    public UserStateStore() : base()
    {
        UserState initialState = new(new());
        this.state = new BehaviorSubject<UserState>(initialState);
    }
    public async Task ChangeState(UserDTO user)
    {
        UserState? nextState = this.state.Value with
        {
            User = user
        };
        this.state.OnNext(nextState);
    }
    public UserDTO GetStateValue()
    {
        return this.state.Value.User;
    }
}
