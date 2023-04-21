using CommunityToolkit.Mvvm.ComponentModel;
using SportSpark.Helpers;
using SportSpark.Services;

namespace SportSpark.ViewModels.Base
{
    public partial class BaseViewModel : ObservableObject
    {
        public INavigation Navigation => Application.Current.MainPage.Navigation;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;

        public static LanguageHelper Language
        {
            get
            {
                return LanguageHelper.Instance;
            }
        }

        protected readonly INavigationService _navigationService;
        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
