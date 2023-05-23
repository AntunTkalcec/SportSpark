using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;
using SportSparkCoreSharedLibrary.DTOs;
using System.Collections.ObjectModel;

namespace SportSpark.ViewModels
{
    [QueryProperty(nameof(User), "User"), QueryProperty(nameof(Events), "Events")]
    public partial class SeeMoreViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserValue))]
        UserDTO user = null;
        public UserDTO UserValue => User;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EventsCollection))]
        ObservableCollection<EventDTO> events = null;
        public ObservableCollection<EventDTO> EventsCollection => Events;
        #endregion
        public SeeMoreViewModel(INavigationService navigationService, IRestService restService) 
            : base(navigationService, restService)
        {
        }

        [RelayCommand]
        async Task GoToUserProfile(UserDTO eventCreator)
        {
            bool userIsNotFriend = !(eventCreator.RequestedFriendships.Any(x => x.SenderId == UserValue.Id || x.ReceiverId == UserValue.Id) ||
                eventCreator.ReceivedFriendships.Any(x => x.SenderId == UserValue.Id || x.ReceiverId == UserValue.Id));
            await _navigationService.NavigateToAsync(nameof(ProfileView), new Dictionary<string, object>
            {
                { "User", eventCreator }, { "SameUser", false }, { "UserIsNotFriend",  userIsNotFriend }, { "UserProfilePicture", eventCreator.ProfileImageData }
            });
        }
    }
}
