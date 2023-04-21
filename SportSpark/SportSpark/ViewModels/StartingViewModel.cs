using CommunityToolkit.Mvvm.Input;

namespace SportSpark.ViewModels
{
    public partial class StartingViewModel
    {
        public StartingViewModel()
        {
        }

        [RelayCommand]
        void SignIn()
        {
            Preferences.Set("welcomed", "1");
            Application.Current.MainPage = new AppShell();
        }
    }
}
