using CommunityToolkit.Mvvm.Input;
using SportSpark.Services;
using SportSpark.ViewModels.Base;
using System.Windows.Input;

namespace SportSpark.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        public ICommand GetUser { get; }
        public ProfileViewModel(INavigationService navigationService, IRestService restService)
            : base(navigationService, restService)
        {
            GetUser = new AsyncRelayCommand(async () => await GetUser());
            GetUser.Execute(null);
            Title = "{User}'s Profile";
        }
    }
}
