using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SportSpark.Models;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;
using SportSpark.Views.Popups;
using SportSparkCoreLibrary.Enums;
using SportSparkCoreSharedLibrary.DTOs;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace SportSpark.ViewModels
{
    public partial class HomeViewModel : BaseViewModel, IRecipient<Message>
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LoggedInUserValue))]
        UserDTO loggedInUser = null;
        public UserDTO LoggedInUserValue => LoggedInUser;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SearchIconCode))]
        string searchIcon = FaSolid.MagnifyingGlass;
        public string SearchIconCode => SearchIcon;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SearchInputValue))]
        string searchInput = string.Empty;
        public string SearchInputValue => SearchInput;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MenuIconCode))]
        string menuIcon = FaSolid.BarsStaggered;
        public string MenuIconCode => MenuIcon;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EventsNearUserCollection))]
        ObservableCollection<EventDTO> eventsNearUser;
        public ObservableCollection<EventDTO> EventsNearUserCollection => EventsNearUser;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserLocationValue))]
        Location location = null;
        public Location UserLocationValue => Location;

        public ICommand GetEventsNearUser { get; }

        public ICommand GetUserLocation { get; }
        #endregion
        public HomeViewModel(INavigationService navigationService, IRestService restService)
            : base(navigationService, restService)
        {
            GetUserLocation = new AsyncRelayCommand(GetUserLocationAsync);
            GetUserLocation.Execute(null);
            GetEventsNearUser = new AsyncRelayCommand(async () => await GetEventsNearUserAsync(null));
            GetEventsNearUser.Execute(null);
            WeakReferenceMessenger.Default.Register(this);
        }

        [RelayCommand]
        async Task RefreshPageAsync(string searchText = null)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                var result = await Application.Current.MainPage.ShowPopupAsync(new ErrorPopup("No internet connection"));

                if (result is bool boolResult)
                {
                    if (boolResult)
                    {
                        await RefreshPageAsync(searchText);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            EventsNearUser?.Clear();

            await GetEventsNearUserAsync(searchText);
        }

        [RelayCommand]
        async Task GoToUserProfile(UserDTO eventCreator)
        {
            bool userIsNotFriend = !(eventCreator.RequestedFriendships.Any(x => x.SenderId == LoggedInUserValue.Id || x.ReceiverId == LoggedInUserValue.Id) ||
                eventCreator.ReceivedFriendships.Any(x => x.SenderId == LoggedInUserValue.Id || x.ReceiverId == LoggedInUserValue.Id));
            await _navigationService.NavigateToAsync(nameof(ProfileView), new Dictionary<string, object>
            {
                { "User", eventCreator }, { "SameUser", false }, { "UserIsNotFriend",  userIsNotFriend }, { "UserProfilePicture", eventCreator.ProfileImageData }
            });
        }

        [RelayCommand]
        async Task SeeMoreAsync()
        {
            if (Location != null)
            {
                LatLongWrapperDTO wrapper = new()
                {
                    Latitude = Location.Latitude,
                    Longitude = Location.Longitude
                };

                await _navigationService.NavigateToAsync(nameof(SeeMoreView), new Dictionary<string, object>
                {
                    { "User", LoggedInUserValue }, 
                    { "Events", new ObservableCollection<EventDTO>(await _restService.GetEventsNearUserAsync(LoggedInUserValue.DesiredRadius * 2.5, wrapper)) }
                });
            }
            else
            {
                await Application.Current.MainPage.ShowPopupAsync(new ErrorPopup("Couldn't get your location."));
            }
        }

        public async void Receive(Message message)
        {
            switch (message.Value)
            {
                case "SignOut":
                    Preferences.Set("access_token", "");
                    Preferences.Set("refresh_token", "");
                    Preferences.Set("token_expiration", "");
                    await Shell.Current.Navigation.PopToRootAsync();
                    Application.Current.MainPage = new AppShell();
                    break;
                case "GoToProfile":
                    await _navigationService.NavigateToAsync(nameof(ProfileView), new Dictionary<string, object>
                    {
                        { "User", LoggedInUserValue }, { "SameUser", true }, { "UserIsNotFriend", true }, { "UserProfilePicture", LoggedInUserValue.ProfileImageData }
                    });
                    break;
                case "GoToFriends":
                    await _navigationService.NavigateToAsync(nameof(FriendsView), new Dictionary<string, object>
                    {
                        { "ReceivedFriendships", new ObservableCollection<FriendshipDTO>(await _restService.GetReceivedFriendshipsForUserAsync(LoggedInUserValue.Id)) }
                    });
                    break;
                case "GoToFriendsList":
                    //await GetUser(); commented because of catastrophic imagesource bug
                    List<FriendshipDTO> friendships = LoggedInUserValue.RequestedFriendships.Where(x => x.Status == (int)FriendshipStatus.Confirmed).ToList();
                    friendships.AddRange(LoggedInUserValue.ReceivedFriendships.Where(x => x.Status == (int)FriendshipStatus.Confirmed));
                    await _navigationService.NavigateToAsync(nameof(FriendsListView), new Dictionary<string, object>
                    {
                        { "Friendships", new ObservableCollection<FriendshipDTO>(friendships) }, { "User", LoggedInUserValue }
                    });
                    break;
            }
        }

        public async Task GetEventsNearUserAsync(string text = null)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                var result = await Application.Current.MainPage.ShowPopupAsync(new ErrorPopup("No internet connection"));

                if (result is bool boolResult)
                {
                    if (boolResult)
                    {
                        await RefreshPageAsync();
                    }
                    else
                    {
                        return;
                    }
                }
            }

            try
            {
                IsBusy = true;

                await GetUser();

                if (Location != null)
                {
                    LatLongWrapperDTO wrapper = new()
                    {
                        Latitude = Location.Latitude,
                        Longitude = Location.Longitude
                    };

                    if (string.IsNullOrEmpty(text))
                    {
                        EventsNearUser = new ObservableCollection<EventDTO>(await _restService.GetEventsNearUserAsync(LoggedInUserValue.DesiredRadius, wrapper));
                    }
                    else
                    {
                        EventsNearUser = new ObservableCollection<EventDTO>(await _restService.GetEventsByTermAsync(LoggedInUserValue.DesiredRadius, wrapper, text));
                    }
                }
            }
            catch (Exception ex)
            {
                await Toast.Make("An unknown error occurred.").Show();
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task GetUser()
        {
            //maui catastrophic imagesource bug - https://github.com/dotnet/maui/issues/14052
            LoggedInUser = await _restService.GetLoggedInUser();
        }

        private async Task GetUserLocationAsync()
        {
            GeolocationRequest request = new(GeolocationAccuracy.Best, TimeSpan.FromSeconds(60));

            Location = await Geolocation.Default.GetLocationAsync(request);
        }
    }
}
