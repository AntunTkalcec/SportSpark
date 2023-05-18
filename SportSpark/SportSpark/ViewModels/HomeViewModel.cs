﻿using CommunityToolkit.Maui.Alerts;
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

        public ICommand GetEventsNearUser { get; }
        #endregion
        public HomeViewModel(INavigationService navigationService, IRestService restService)
            : base(navigationService, restService)
        {
            GetEventsNearUser = new AsyncRelayCommand(async () => await GetEventsNearUserAsync(null));
            GetEventsNearUser.Execute(null);
            WeakReferenceMessenger.Default.Register(this);
        }

        [RelayCommand]
        async Task OpenMenuAsync()
        {
            await _navigationService.NavigateToAsync(nameof(MenuView));
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
                { "User", eventCreator }, { "SameUser", false }, { "UserIsNotFriend",  userIsNotFriend },
            });
        }

        public async void Receive(Message message)
        {
            switch (message.Value)
            {
                case "SignOut":
                    Preferences.Set("access_token", "");
                    Preferences.Set("refresh_token", "");
                    Preferences.Set("token_expiration", "");
                    Application.Current.MainPage = new AppShell();
                    break;
                case "GoToProfile":
                    await _navigationService.NavigateToAsync(nameof(ProfileView), new Dictionary<string, object>
                    {
                        { "User", LoggedInUserValue }, { "SameUser", true }, { "UserIsNotFriend", true }
                    });
                    break;
                case "GoToFriends":
                    //go to friends page
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

                GeolocationRequest request = new(GeolocationAccuracy.Best, TimeSpan.FromSeconds(60));

                Location location = await Geolocation.Default.GetLocationAsync(request);

                if (location != null)
                {
                    LatLongWrapperDTO wrapper = new()
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
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
    }
}
