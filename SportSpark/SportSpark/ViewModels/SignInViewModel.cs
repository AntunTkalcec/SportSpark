using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Helpers;
using SportSpark.Models.Font;
using SportSpark.ViewModels.Base;
using SportSpark.Views;

namespace SportSpark.ViewModels
{
    public partial class SignInViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPassword))]
        bool isNotPassword = false;
        public bool IsPassword => !IsNotPassword;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PasswordVisibilityCode))]
        string passwordVisibility = FaSolid.Eye;
        public string PasswordVisibilityCode => PasswordVisibility;

        public SignInViewModel()
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
            await Navigation.PushAsync(new RegisterView());
        }
    }
}
