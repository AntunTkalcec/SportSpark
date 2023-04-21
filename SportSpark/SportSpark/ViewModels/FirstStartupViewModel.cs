using CommunityToolkit.Mvvm.Input;
using SportSpark.Views;

namespace SportSpark.ViewModels
{
    public partial class FirstStartupViewModel
    {
        public FirstStartupViewModel()
        {
        }

        [RelayCommand]
        async Task GetStartedAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new StartingView());
        }
    }
}
