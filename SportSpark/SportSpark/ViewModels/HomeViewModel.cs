using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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

        public async void Receive(Message message)
        {
            Preferences.Set("access_token", "");
            Preferences.Set("refresh_token", "");
            Preferences.Set("token_expiration", "");

            Application.Current.MainPage = new AppShell();
        }
    }
}
