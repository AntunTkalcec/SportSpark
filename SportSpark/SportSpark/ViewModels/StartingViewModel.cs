using CommunityToolkit.Mvvm.Input;
using SportSpark.Helpers;

namespace SportSpark.ViewModels
{
    public partial class StartingViewModel
    {
        public LanguageHelper Language
        {
            get
            {
                return LanguageHelper.Instance;
            }
        }
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
