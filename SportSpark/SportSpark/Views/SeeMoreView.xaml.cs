using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class SeeMoreView : ContentPage
{
	private readonly SeeMoreViewModel _viewModel;
	public SeeMoreView(SeeMoreViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_viewModel = vm;
	}
}