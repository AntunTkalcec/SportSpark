using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSparkCoreLibrary.Enums;
using SportSparkCoreSharedLibrary.DTOs;
using System.Collections.ObjectModel;

namespace SportSpark.ViewModels
{
    [QueryProperty(nameof(ReceivedFriendships), "ReceivedFriendships")]
    public partial class FriendsViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ReceivedFriendshipsCollection))]
        ObservableCollection<FriendshipDTO> receivedFriendships;
        public ObservableCollection<FriendshipDTO> ReceivedFriendshipsCollection => ReceivedFriendships;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StarCode))]
        string star = FaSolid.Star;
        public string StarCode => Star;
        #endregion
        public FriendsViewModel(INavigationService navigationService, IRestService restService) : base(navigationService, restService)
        {
        }

        [RelayCommand]
        async Task ConfirmRequestAsync(FriendshipDTO friendship)
        {
            friendship.Status = (int)FriendshipStatus.Confirmed;

            if (await _restService.UpdateFriendshipStatus(friendship))
            {
                ReceivedFriendships.Remove(friendship);
            }
        }

        [RelayCommand]
        async Task DenyRequestAsync(FriendshipDTO friendship)
        {
            friendship.Status = (int)FriendshipStatus.Denied;

            if (await _restService.UpdateFriendshipStatus(friendship))
            {
                ReceivedFriendships.Remove(friendship);
            }
        }
    }
}
