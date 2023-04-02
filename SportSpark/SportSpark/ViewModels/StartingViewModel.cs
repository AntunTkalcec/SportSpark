using CommunityToolkit.Mvvm.Input;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;

namespace SportSpark.ViewModels
{
    public partial class StartingViewModel : BaseViewModel
    {
        public StartingViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        [RelayCommand]
        async Task RegisterAsync()
        {
            await _navigationService.NavigateToAsync(nameof(RegisterView));
        }

        [RelayCommand]
        async Task SignInAsync()
        {
            await _navigationService.NavigateToAsync(nameof(SignInView));
        }
    }
}
