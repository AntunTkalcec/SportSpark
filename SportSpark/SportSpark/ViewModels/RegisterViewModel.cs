using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSparkInfrastructureLibrary.Helpers;

namespace SportSpark.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

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

        public RegisterViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        async Task RegisterAsync()
        {
            var test1 = FirstNameValue;
            var test2 = LastNameValue;
            var test3 = UsernameValue;
            var test4 = EmailValue;
            var test5 = PasswordValue;
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
