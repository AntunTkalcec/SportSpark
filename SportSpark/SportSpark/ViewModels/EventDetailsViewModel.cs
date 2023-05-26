using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSpark.ViewModels
{
    [QueryProperty(nameof(Ev), "Event"), QueryProperty(nameof(LoggedInUser), "LoggedInUser")]
    public partial class EventDetailsViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EventValue))]
        EventDTO ev = null;
        public EventDTO EventValue => Ev;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LoggedInUserValue))]
        UserDTO loggedInUser = null;
        public UserDTO LoggedInUserValue => LoggedInUser;
        #endregion
        public EventDetailsViewModel(INavigationService navigationService, IRestService restService) : base(navigationService, restService)
        {
        }

        [RelayCommand]
        async Task ChangeEventStatusAsync(EventDTO entity)
        {
            entity.Active = !entity.Active;

            //update event
        }

        [RelayCommand]
        async Task ViewProfileAsync(UserDTO user)
        {
            await _navigationService.NavigateToAsync(nameof(ProfileView), new Dictionary<string, object>
            {
                { "User", user }, { "SameUser", false }, { "UserIsNotFriend",  false }, { "UserProfilePicture", user.ProfileImageData }
            });
        }
    }
}
