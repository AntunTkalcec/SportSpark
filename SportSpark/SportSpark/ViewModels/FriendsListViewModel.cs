using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;
using SportSparkCoreLibrary.Enums;
using SportSparkCoreSharedLibrary.DTOs;
using System.Collections.ObjectModel;

namespace SportSpark.ViewModels
{
    [QueryProperty(nameof(Friendships), "Friendships"), QueryProperty(nameof(User), "User")]
    public partial class FriendsListViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FriendshipsCollection))]
        ObservableCollection<FriendshipDTO> friendships;
        public ObservableCollection<FriendshipDTO> FriendshipsCollection => Friendships;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserValue))]
        UserDTO user = null;
        public UserDTO UserValue => User;
        #endregion
        public FriendsListViewModel(INavigationService navigationService, IRestService restService) : base(navigationService, restService)
        {
        }

        [RelayCommand]
        async Task ViewProfileAsync(FriendshipDTO friendship)
        {
            UserDTO userToSend = friendship.SenderId == UserValue.Id ? friendship.Receiver : friendship.Sender;

            await _navigationService.NavigateToAsync(nameof(ProfileView), new Dictionary<string, object>
            {
                { "User", userToSend }, { "SameUser", false }, { "UserIsNotFriend",  false },
            });
        }

        [RelayCommand]
        async Task RemoveFriendAsync(FriendshipDTO friendship)
        {
            friendship.Status = (int)FriendshipStatus.Denied;

            if (await _restService.UpdateFriendshipStatus(friendship))
            {
                Friendships.Remove(friendship);
            }
        }
    }
}
