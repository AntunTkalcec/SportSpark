using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;
using SportSpark.Views.Popups;
using SportSparkCoreSharedLibrary.DTOs;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SportSpark.ViewModels
{
    [QueryProperty(nameof(User), "User"), QueryProperty(nameof(SameUser), "SameUser"), QueryProperty(nameof(UserIsNotFriend), "UserIsNotFriend"),
        QueryProperty(nameof(ProfileImage), "UserProfilePicture")]
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
        bool userIsNotFriend;

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

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ProfileImageValue))]
        byte[] profileImage = Array.Empty<byte>();
        public byte[] ProfileImageValue => ProfileImage;
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
                bool success = await _restService.AddAsFriendAsync(UserValue.Id);
                if (success)
                {
                    await Toast.Make("Friendship request sent!").Show();
                }
                else
                {
                    var res = await Application.Current.MainPage.ShowPopupAsync(new ErrorPopup("Friendship request couldn't be sent"));

                    if (res is bool boolResult)
                    {
                        if (boolResult)
                        {
                            await EditOrAddAsync();
                        }
                    }
                }
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

        [RelayCommand]
        async Task CreateEventAsync()
        {
            await _navigationService.NavigateToAsync(nameof(CreateEventView), new Dictionary<string, object>
            {
                { "User", UserValue }
            });
        }

        [RelayCommand]
        async Task ChangeProfilePictureAsync()
        {
            var res = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Choose a new profile picture",
                FileTypes = FilePickerFileType.Images
            });

            if (res is null)
            {
                return;
            }

            var stream = await res.OpenReadAsync();

            byte[] imageData;
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            imageData = memoryStream.ToArray();

            ProfileImage = imageData;

            bool answer = await Application.Current.MainPage.DisplayAlert("Are you sure", "Keep this profile picture?", "Yes", "No");
            if (answer)
            {
                await _restService.CreateNewProfilePictureAsync(new DocumentDTO
                {
                    ImageTitle = res.FileName,
                    ImageData = imageData,
                    Owner = UserValue
                });
                // save image to db
                return;
            }

            ProfileImage = UserValue.ProfileImageData;
        }

        [RelayCommand]
        async Task RateUserAsync()
        {
            var res = await Application.Current.MainPage.ShowPopupAsync(new RateUserPopup());

            if (res is int intResult)
            {
                await _restService.RateUserAsync(UserValue.Id, intResult);
            }
        }

        [RelayCommand]
        async Task GoToEventDetailsAsync(EventDTO entity)
        {
            await _navigationService.NavigateToAsync(nameof(EventDetailsView), new Dictionary<string, object>
            {
                { "Event", entity }, { "LoggedInUser", null }, { "SameUser", false }
            });
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
