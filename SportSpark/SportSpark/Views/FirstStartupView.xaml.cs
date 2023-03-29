using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class FirstStartupView : ContentPage
{
	public FirstStartupView(FirstStartupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}