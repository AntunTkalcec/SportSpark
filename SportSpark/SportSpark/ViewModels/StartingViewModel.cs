using CommunityToolkit.Mvvm.Input;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;

namespace SportSpark.ViewModels
{
    public partial class StartingViewModel : BaseViewModel
    {
        public StartingViewModel()
        {
        }

        [RelayCommand]
        async void SignIn()
        {
            await Navigation.PushAsync(new SignInView());
        }
    }
}
