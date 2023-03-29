using CommunityToolkit.Mvvm.Input;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using SportSpark.Views;

namespace SportSpark.ViewModels
{
    public partial class FirstStartupViewModel : BaseViewModel
    {
        public FirstStartupViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        [RelayCommand]
        async Task GetStartedAsync()
        {
            await _navigationService.NavigateToAsync(nameof(StartingView));
        }
    }
}
