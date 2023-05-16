using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Models;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views.Popups;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSpark.ViewModels
{
    [QueryProperty(nameof(User), "User")]
    public partial class CreateEventViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NewEventValue))]
        EventDTO newEvent = null;
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
        [NotifyPropertyChangedFor(nameof(SelectedPrivacyValue))]
        EventPrivacy selectedPrivacy = null;
        public EventPrivacy SelectedPrivacyValue => SelectedPrivacy;

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
        }

        [RelayCommand]
        async Task OpenLocationSelectionPopupAsync()
        {
            var res = await Application.Current.MainPage.ShowPopupAsync(new LocationSelectionPopup());

            if (res is List<double> result)
            {
                var nekaj = res;
            }
        }

        [RelayCommand]
        async Task CreateEventAsync()
        {
            //
        }
    }
}
