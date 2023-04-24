using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    private void MenuClicked(object sender, TappedEventArgs e)
    {
        _ = MainContainer.TranslateTo(this.Width * 0.8, 0, 250, Easing.CubicIn);
        _ = MainContainer.FadeTo(0.8, 250);
    }

    private async void MainContainerTapped(object sender, TappedEventArgs e)
    {
        await CloseMenu();
    }

    private void searchEntry_Completed(object sender, EventArgs e)
    {

    }

    private async Task CloseMenu()
    {
        _ = MainContainer.FadeTo(1, 250);
        await MainContainer.TranslateTo(0, 0, 250, Easing.CubicIn);
    }
}