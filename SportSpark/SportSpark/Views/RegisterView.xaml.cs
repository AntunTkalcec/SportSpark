using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class RegisterView : ContentPage
{
	private RegisterViewModel vm;
	public RegisterView()
	{
		InitializeComponent();
		BindingContext = vm = new RegisterViewModel();
	}
}