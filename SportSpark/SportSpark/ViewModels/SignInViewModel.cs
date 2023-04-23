using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;

namespace SportSpark.ViewModels
{
    public partial class SignInViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPassword))]
        bool isNotPassword = false;
        public bool IsPassword => !IsNotPassword;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PasswordVisibilityCode))]
        string passwordVisibility = FaSolid.Eye;
        public string PasswordVisibilityCode => PasswordVisibility;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UsernameOrEmailValue))]
        string usernameOrEmail = string.Empty;
        public string UsernameOrEmailValue => UsernameOrEmail;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PasswordValue))]
        string password = string.Empty;
        public string PasswordValue => Password;
        #endregion

        public SignInViewModel(INavigationService navigationService, IRestService restService) 
            : base(navigationService, restService)
        {
        }

        [RelayCommand]
        void ChangePasswordVisibilityIcon()
        {
            IsNotPassword = !IsNotPassword;
            if (!IsNotPassword)
            {
                PasswordVisibility = FaSolid.Eye;
            }
            else
            {
                PasswordVisibility = FaSolid.EyeSlash;
            }
        }

        [RelayCommand]
        async void Register()
        {
            await _navigationService.NavigateToAsync(nameof(RegisterView));
        }

        [RelayCommand]
        async Task SignInAsync()
        {
            if (await _restService.SignInAsync(UsernameOrEmailValue, PasswordValue))
            {
                await Toast.Make("Login successful", CommunityToolkit.Maui.Core.ToastDuration.Short, 24).Show();
                await _navigationService.NavigateToAsync(nameof(HomeView));
            }
            else
            {
                await Toast.Make("No user found with that information", CommunityToolkit.Maui.Core.ToastDuration.Short, 24).Show();
            }
        }
    }
}
