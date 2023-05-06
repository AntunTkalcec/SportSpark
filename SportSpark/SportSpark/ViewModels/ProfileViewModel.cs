using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views.Popups;
using SportSparkCoreSharedLibrary.DTOs;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace SportSpark.ViewModels
{
    [QueryProperty(nameof(User), "User"), QueryProperty(nameof(SameUser), "SameUser")]
    public partial class ProfileViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserValue))]
        UserDTO user = null;
        public UserDTO UserValue => User;

        [ObservableProperty]
        bool sameUser;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StarCode))]
        string star = FaSolid.Star;
        public string StarCode => Star;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EditableValue))]
        bool editable = false;
        public bool EditableValue => !Editable;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ProfileSelectedValue))]
        bool profileSelected = true;
        public bool ProfileSelectedValue => ProfileSelected;

        [ObservableProperty]
        double eventsLayoutDefaultTranslation = double.NaN;
        public double EventsLayoutDefaultTranslationValue => EventsLayoutDefaultTranslation;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserEventsValue))]
        ObservableCollection<EventDTO> userEvents;
        public ObservableCollection<EventDTO> UserEventsValue => UserEvents;
        #endregion

        public ProfileViewModel(INavigationService navigationService, IRestService restService)
            : base(navigationService, restService)
        {
            EventsLayoutDefaultTranslation = DeviceDisplay.Current.MainDisplayInfo.Width * 2;
        }

        [RelayCommand]
        async Task EditOrAddAsync()
        {
            if (SameUser)
            {
                if (Editable)
                {
                    await EditUserAsync();
                }
                Editable = !Editable;
            }
            else
            {
                //add friend
            } 
        }

        [RelayCommand]
        public async Task EditUserAsync()
        {
            if (await _restService.UpdateUserInfoAsync(UserValue))
            {
                await Toast.Make("User successfully updated.").Show();
            }
            else
            {
                var res = await Application.Current.MainPage.ShowPopupAsync(new ErrorPopup("User couldn't be updated"));

                if (res is bool boolResult)
                {
                    if (boolResult)
                    {
                        await EditUserAsync();
                    }
                }
            }
        }

        [RelayCommand]
        public void ShowProfile()
        {
            ProfileSelected = true;
        }

        [RelayCommand]
        public async Task ShowEvents()
        {
            ProfileSelected = false;
            await GetUserEventsAsync();
        }

        private async Task GetUserEventsAsync()
        {
            try
            {
                UserEvents = new ObservableCollection<EventDTO>(await _restService.GetUserEventsAsync(UserValue.Id));
            }
            catch (Exception ex) 
            {
                await Toast.Make("An unknown error occurred.").Show();
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
