using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using SportSpark.Models;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;
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

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LoggedInUserValue))]
        UserDTO loggedInUser = null;
        public UserDTO LoggedInUserValue => LoggedInUser;

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
            EventsNearUser.Clear();
            await GetEventsNearUserAsync();
        }

        private async Task GetUser()
        {
            LoggedInUser = await _restService.GetLoggedInUser();
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
                    await _navigationService.NavigateToAsync(nameof(ProfileView));
                    break;
                case "GetLoggedInUser":
                    await GetUser();
                    break;
            }
        }

        private async Task GetEventsNearUserAsync()
        {
            try
            {
                IsBusy = true;
                EventsNearUser = new ObservableCollection<EventDTO>(await _restService.GetEventsNearUserAsync());
            }
            catch (Exception ex)
            {
                Toast.Make("An unknown error occurred.");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        } 
    }
}
