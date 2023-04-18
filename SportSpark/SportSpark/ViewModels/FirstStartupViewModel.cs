using CommunityToolkit.Mvvm.Input;
using SportSpark.ViewModels.Base;
using SportSpark.Views;

namespace SportSpark.ViewModels
{
    public partial class FirstStartupViewModel : BaseViewModel
    {
        public FirstStartupViewModel()
        {
        }

        [RelayCommand]
        async Task GetStartedAsync()
        {
            await Navigation.PushAsync(new StartingView());
        }
    }
}
