using CommunityToolkit.Mvvm.ComponentModel;
using SportSpark.Models.Font;
using SportSpark.Services;
using SportSpark.ViewModels.Base;

namespace SportSpark.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        string searchIcon = FaSolid.MagnifyingGlass;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SearchInputValue))]
        string searchInput = string.Empty;
        public string SearchInputValue => SearchInput;
        #endregion
        public HomeViewModel(INavigationService navigationService, IRestService restService)
            : base(navigationService, restService)
        {
        }


    }
}
