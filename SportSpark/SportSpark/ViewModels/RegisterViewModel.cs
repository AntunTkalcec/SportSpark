using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Helpers;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSpark.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
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
        [NotifyPropertyChangedFor(nameof(PasswordErrorVisible))]
        bool passwordError = false;
        public bool PasswordErrorVisible => PasswordError;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FirstNameValue))]
        string firstName = string.Empty;
        public string FirstNameValue => FirstName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LastNameValue))]
        string lastName = string.Empty;
        public string LastNameValue => LastName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UsernameValue))]
        string username = string.Empty;
        public string UsernameValue => Username;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EmailValue))]
        string email = string.Empty;
        public string EmailValue => Email;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PasswordValue))]
        string password = string.Empty;
        public string PasswordValue => Password;
        #endregion

        public RegisterViewModel(INavigationService navigationService, IRestService restService) 
            : base(navigationService, restService)
        {
        }

        [RelayCommand]
        async Task RegisterAsync()
        {
            UserDTO userDTO = new()
            {
                FirstName = FirstNameValue,
                LastName = LastNameValue,
                Email = EmailValue,
                UserName = UsernameValue,
                Password = PasswordValue
            };

            if (await _restService.RegisterAsync(userDTO))
            {
                await Toast.Make("Registration successful", CommunityToolkit.Maui.Core.ToastDuration.Short, 24).Show();
                await _navigationService.NavigateToAsync("..");
            }
            else
            {
                await Toast.Make("Registration failed", CommunityToolkit.Maui.Core.ToastDuration.Short, 24).Show();
            }
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

        public bool CheckFirstName()
        {
            if (FirstNameValue.Length < 20)
                return true;
            return false;
        }

        public bool CheckLastName()
        {
            if (LastNameValue.Length < 20)
                return true;
            return false;
        }

        public bool CheckUserName()
        {
            if (RegexHelper.UserNameRegex().Match(UsernameValue).Success)
            {
                return true;
            }
            return false;
        }

        public bool CheckEmail()
        {
            if (RegexHelper.EmailRegex().Match(EmailValue).Success)
            {
                return true;
            }
            return false;
        }

        public bool CheckPassword()
        {
            if (RegexHelper.PasswordRegex().Match(PasswordValue).Success)
            {
                PasswordError = false;
                return true;
            }
            PasswordError = true;
            return false;
        }
    }
}
