using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class StartingView : ContentPage
{
	public StartingView(StartingViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}