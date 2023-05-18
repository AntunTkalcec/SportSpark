using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Models;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views.Popups;
using SportSparkCoreSharedLibrary.DTOs;
using System.Windows.Input;

namespace SportSpark.ViewModels
{
    [QueryProperty(nameof(User), "User")]
    public partial class CreateEventViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NewEventValue))]
        EventDTO newEvent = new();
        public EventDTO NewEventValue => NewEvent;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserValue))]
        UserDTO user = null;
        public UserDTO UserValue => User;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LatitudeValue))]
        double latitude;
        public double LatitudeValue => Latitude;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LongitudeValue))]
        double longitude;
        public double LongitudeValue => Longitude;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EventPrivaciesList))]
        List<EventPrivacy> eventPrivacies = new();
        public List<EventPrivacy> EventPrivaciesList => EventPrivacies;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EventTypesList))]
        List<EventTypeDTO> eventTypes = new();
        public List<EventTypeDTO> EventTypesList => EventTypes;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EventRepeatTypesList))]
        List<EventRepeatTypeDTO> eventRepeatTypes = new();
        public List<EventRepeatTypeDTO> EventRepeatTypesList => EventRepeatTypes;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SelectedPrivacyValue))]
        EventPrivacy selectedPrivacy = null;
        public EventPrivacy SelectedPrivacyValue => SelectedPrivacy;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LocationNotChosenValue))]
        bool locationNotChosen = true;
        public bool LocationNotChosenValue => LocationNotChosen;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LocationCoordinatesValue))]
        string locationCoordinates = string.Empty;
        public string LocationCoordinatesValue => LocationCoordinates;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ChosenTimeValue))]
        TimeSpan chosenTime = TimeSpan.Zero;
        public TimeSpan ChosenTimeValue => ChosenTime;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ChosenDateValue))]
        DateTime chosenDate = DateTime.MinValue;
        public DateTime ChosenDateValue => ChosenDate;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TodayValue))]
        DateTime today = DateTime.UtcNow;
        public DateTime TodayValue => Today;

        public ICommand GetEventTypes { get; }
        public ICommand GetEventRepeatTypes { get; }
        #endregion
        public CreateEventViewModel(INavigationService navigationService, IRestService restService) : base(navigationService, restService)
        {
            EventPrivacies.AddRange(new List<EventPrivacy>()
            {
                new EventPrivacy
                {
                    Name = "Public",
                    Value = 0
                },
                new EventPrivacy
                {
                    Name = "Friends",
                    Value = 1
                },
                new EventPrivacy
                {
                    Name = "Selected",
                    Value = 2
                }
            });
            GetEventTypes = new AsyncRelayCommand(GetEventTypesAsync);
            GetEventTypes.Execute(null);
        }

        [RelayCommand]
        async Task OpenLocationSelectionPopupAsync()
        {
            var res = await Application.Current.MainPage.ShowPopupAsync(new LocationSelectionPopup());

            if (res is List<double> result)
            {
                LocationNotChosen = false;
                LocationCoordinates = $"{result[0]}, {result[1]}";
                NewEvent.Lat = (decimal?)result[0];
                NewEvent.Long = (decimal?)result[1];
            }
        }

        [RelayCommand]
        async Task CreateEventAsync()
        {
            NewEvent.User = UserValue;
            NewEvent.Active = true;
            NewEvent.Time = ChosenDateValue.Add(ChosenTimeValue);
            NewEvent.Privacy = SelectedPrivacyValue.Value;

            await _restService.CreateNewEventAsync(NewEventValue);
            await _navigationService.NavigateToAsync("..");
        }

        private async Task GetEventTypesAsync()
        {
            EventTypes = await _restService.GetEventTypesAsync();
            EventRepeatTypes = await _restService.GetEventRepeatTypesAsync();
        }
    }
}
