using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class FriendsView : ContentPage
{
	private readonly FriendsViewModel _viewModel;
	public FriendsView(FriendsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_viewModel = vm;
	}
}