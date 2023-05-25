using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSparkCoreSharedLibrary.DTOs;
using System.Collections.ObjectModel;

namespace SportSpark.ViewModels
{
    [QueryProperty(nameof(NewEvent), "NewEvent"), QueryProperty(nameof(Friendships), "Friendships")]
    public partial class EventVisibleToViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NewEventValue))]
        EventDTO newEvent;
        public EventDTO NewEventValue => NewEvent;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FriendshipsCollection))]
        ObservableCollection<FriendshipDTO> friendships;
        public ObservableCollection<FriendshipDTO> FriendshipsCollection => Friendships;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SelectedFriendsCollection))]
        ObservableCollection<object> selectedFriends = new();
        public ObservableCollection<object> SelectedFriendsCollection => SelectedFriends;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StarCode))]
        string star = FaSolid.Star;
        public string StarCode => Star;

        public EventVisibleToViewModel(INavigationService navigationService, IRestService restService) : base(navigationService, restService)
        {
        }

        [RelayCommand]
        async Task CreateEventAsync()
        {
            List<int> selectedFriends = SelectedFriendsCollection.Select(x => (x as FriendshipDTO).SenderId).ToList();
            selectedFriends.AddRange(SelectedFriendsCollection.Select(x => (x as FriendshipDTO).ReceiverId));
            NewEvent.ValidUserIds = selectedFriends;

            await _restService.CreateNewEventAsync(NewEventValue);
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
