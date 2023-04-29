using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using SportSpark.Views;
using SportSpark.Views.Popups;

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
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new StartingView());
            }
            else
            {
                await Application.Current.MainPage.ShowPopupAsync(new LocationPermissionPopup());
            }
        }
    }
}
