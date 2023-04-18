using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class StartingView : ContentPage
{
	private StartingViewModel vm;
	public StartingView()
	{
		InitializeComponent();
		BindingContext = vm = new StartingViewModel();
	}
}