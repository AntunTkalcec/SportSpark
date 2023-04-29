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

        public ObservableCollection<EventDTO> EventsNearUser { get; } = new();

        [ObservableProperty]
        string firstName = JsonConvert.DeserializeObject<UserDTO>(Preferences.Get("userInfo", "")).FirstName;
        public string FirstNameValue => FirstName;
        #endregion
        public HomeViewModel(INavigationService navigationService, IRestService restService)
            : base(navigationService, restService)
        {
            WeakReferenceMessenger.Default.Register(this);
        }

        [RelayCommand]
        async Task OpenMenuAsync()
        {
            await _navigationService.NavigateToAsync(nameof(MenuView));
        }

        public void Receive(Message message)
        {
            if (message.Value == "SignOut")
            {
                Preferences.Set("access_token", "");
                Preferences.Set("refresh_token", "");
                Preferences.Set("token_expiration", "");

                Application.Current.MainPage = new AppShell();
            }
        }
    }
}
