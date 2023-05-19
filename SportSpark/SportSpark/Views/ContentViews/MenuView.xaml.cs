using CommunityToolkit.Mvvm.Messaging;
using SportSpark.Models;

namespace SportSpark.Views;

public partial class MenuView : ContentView
{
	public MenuView()
	{
		InitializeComponent();
	}

    private void CloseMenu(object sender, TappedEventArgs e)
    {
		WeakReferenceMessenger.Default.Send(new Message("CloseMenu"));
    }

    private void SignOut(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new Message("SignOut"));
    }

    private void GoToProfile(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new Message("GoToProfile"));
    }

    private void GoToFriends(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new Message("GoToFriends"));
    }

    private void GoToFriendsList(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new Message("GoToFriendsList"));
    }
}