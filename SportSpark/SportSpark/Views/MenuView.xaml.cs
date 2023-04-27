using CommunityToolkit.Mvvm.Messaging;
using SportSpark.Models;

namespace SportSpark.Views;

public partial class MenuView : ContentView
{
	public MenuView()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		WeakReferenceMessenger.Default.Send(new Message("CloseMenu"));
    }

    private void SignOut(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new Message("SignOut"));
    }
}