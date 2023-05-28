using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Messaging;
using SportSpark.Helpers;
using SportSpark.Models;
using System.Globalization;

namespace SportSpark.Views;

public partial class MenuView : ContentView
{
    public LanguageHelper Language
    {
        get
        {
            return LanguageHelper.Instance;
        }
    }
    public MenuView()
	{
		InitializeComponent();
        BindingContext = this;
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

    private async void ChangeLanguage(object sender, EventArgs e)
    {
        string currentLanguage = Preferences.Get("Language", "en-US");
       
        if (currentLanguage == "en-US")
        {
            Preferences.Set("Language", "hr-HR");
            CultureInfo newCulture = new("hr-HR");
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;
            LanguageHelper.Instance.SetCultureInfo(newCulture);
            await Toast.Make("Jezik je postavljen na Hrvatski.").Show();
        }
        else
        {
            Preferences.Set("Language", "en-US");
            CultureInfo newCulture = new("en-US");
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;
            LanguageHelper.Instance.SetCultureInfo(newCulture);
            await Toast.Make("Language has been set to English.").Show();
        }
    }
}