using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}