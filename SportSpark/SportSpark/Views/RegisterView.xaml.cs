using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}