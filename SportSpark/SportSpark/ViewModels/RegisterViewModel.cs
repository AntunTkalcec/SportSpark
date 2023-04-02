using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;

namespace SportSpark.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPassword))]
        bool isNotPassword = false;
        public bool IsPassword => !IsNotPassword;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PasswordVisibilityCode))]
        string passwordVisibility = FaSolid.EyeSlash;
        public string PasswordVisibilityCode => PasswordVisibility;

        public RegisterViewModel(INavigationService navigationService) : base(navigationService)
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
    }
}
