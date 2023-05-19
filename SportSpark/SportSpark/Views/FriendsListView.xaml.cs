using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class FriendsListView : ContentPage
{
	private readonly FriendsListViewModel _viewModel;
	public FriendsListView(FriendsListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_viewModel = vm;
	}
}