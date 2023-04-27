using CommunityToolkit.Mvvm.Messaging;
using SportSpark.Models;
using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        WeakReferenceMessenger.Default.Register<Message>(this, (r, m) =>
        {
            OnMessageReceived(m.Value);
        });
    }

    private void searchEntry_Completed(object sender, EventArgs e)
    {

    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await btmGrid.TranslateTo(0, 0, 250, Easing.SinInOut);
    }

    private async void OnMessageReceived(string value)
    {
        await btmGrid.TranslateTo(0, 360, 250, Easing.SinInOut);
    }
}