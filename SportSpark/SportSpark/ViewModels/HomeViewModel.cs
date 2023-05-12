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
            GetEventsNearUser = new AsyncRelayCommand(GetEventsNearUserAsync);
            GetEventsNearUser.Execute(null);
            WeakReferenceMessenger.Default.Register(this);
        }

        [RelayCommand]
        async Task OpenMenuAsync()
        {
            await _navigationService.NavigateToAsync(nameof(MenuView));
        }

        [RelayCommand]
        async Task RefreshPageAsync()
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

            EventsNearUser?.Clear();

            await GetEventsNearUserAsync();
        }

        [RelayCommand]
        public async Task SearchAsync(string text)
        {
            // TO DO
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

            EventsNearUser?.Clear();

            if (string.IsNullOrEmpty(text))
            {
                await GetEventsNearUserAsync();
            }

            try
            {
                IsBusy = true;
                EventsNearUser = new ObservableCollection<EventDTO>(await _restService.GetEventsByTermAsync(text));
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

        [RelayCommand]
        async Task GoToUserProfile(UserDTO eventCreator)
        {
            await _navigationService.NavigateToAsync(nameof(ProfileView), new Dictionary<string, object>
            {
                { "User", eventCreator }, { "SameUser", false }
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
                    await GetUser();
                    await _navigationService.NavigateToAsync(nameof(ProfileView), new Dictionary<string, object>
                    {
                        { "User", LoggedInUserValue }, { "SameUser", true }
                    });
                    break;
                case "GetLoggedInUser":
                    await GetUser();
                    break;
            }
        }

        private async Task GetEventsNearUserAsync()
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
                EventsNearUser = new ObservableCollection<EventDTO>(await _restService.GetEventsNearUserAsync());
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
