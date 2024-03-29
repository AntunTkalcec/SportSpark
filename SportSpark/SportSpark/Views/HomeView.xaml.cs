using CommunityToolkit.Mvvm.Messaging;
using SportSpark.Models;
using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class HomeView : ContentPage
{
    private readonly HomeViewModel _viewModel;
	public HomeView(HomeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
        WeakReferenceMessenger.Default.Register<Message>(this, (r, m) =>
        {
            OnMessageReceived();
        });
    }

    private async void SearchEntry_Completed(object sender, EventArgs e)
    {
        await _viewModel.GetEventsNearUserAsync((sender as Entry).Text);
    }

    private async void ShowMenu(object sender, TappedEventArgs e)
    {
        await btmGrid.TranslateTo(0, 0, 250, Easing.SinInOut);
    }

    private async void OnMessageReceived()
    {
        await btmGrid.TranslateTo(0, 400, 250, Easing.SinInOut);
    }
}