using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class SignInView : ContentPage
{
	private SignInViewModel vm;

    public SignInView()
	{
		InitializeComponent();
		BindingContext = vm = new SignInViewModel();
	}
}