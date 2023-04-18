using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class FirstStartupView : ContentPage
{
	private FirstStartupViewModel vm;
	public FirstStartupView()
	{
		InitializeComponent();
		BindingContext = vm = new FirstStartupViewModel();
	}
}