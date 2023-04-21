using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class SignInView : ContentPage
{
    public SignInView(SignInViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}