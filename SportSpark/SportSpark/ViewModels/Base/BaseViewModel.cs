using CommunityToolkit.Mvvm.ComponentModel;
using SportSpark.Helpers;
using SportSpark.Services;

namespace SportSpark.ViewModels.Base
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;

        public LanguageHelper Language
        {
            get
            {
                return LanguageHelper.Instance;
            }
        }

        protected readonly INavigationService _navigationService;
        protected readonly IRestService _restService;

        public BaseViewModel(INavigationService navigationService, IRestService restService)
        {
            _navigationService = navigationService;
            _restService = restService;
        }
    }
}
