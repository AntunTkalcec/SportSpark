using CommunityToolkit.Mvvm.Messaging;
using SportSpark.Models;
using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class HomeView : ContentPage
{
    private HomeViewModel viewModel;
	public HomeView(HomeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        viewModel = vm;
        WeakReferenceMessenger.Default.Register<Message>(this, (r, m) =>
        {
            OnMessageReceived(m.Value);
        });
        WeakReferenceMessenger.Default.Send(new Message("GetLoggedInUser"));
    }

    private async void SearchEntry_Completed(object sender, EventArgs e)
    {
        //await viewModel.SearchAsync();
    }

    private async void ShowMenu(object sender, TappedEventArgs e)
    {
        await btmGrid.TranslateTo(0, 0, 250, Easing.SinInOut);
    }

    private async void OnMessageReceived(string value)
    {
        await btmGrid.TranslateTo(0, 360, 250, Easing.SinInOut);
    }
}