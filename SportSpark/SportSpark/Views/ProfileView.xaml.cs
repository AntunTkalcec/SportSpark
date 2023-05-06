using SportSpark.ViewModels;
using System.Diagnostics;

namespace SportSpark.Views;

public partial class ProfileView : ContentPage
{
	ProfileViewModel viewModel;
	public ProfileView(ProfileViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		viewModel = vm;
	}

    private async void ProfileLabelTapped(object sender, TappedEventArgs e)
    {
        try
        {
            ProfileLabel.FontAttributes = FontAttributes.Bold;
            EventsLabel.FontAttributes = FontAttributes.None;
            ProfileLayout.TranslateTo(0, 0, 250, Easing.SinInOut);
            await EventsLayout.TranslateTo(DeviceDisplay.Current.MainDisplayInfo.Width * 2, 0, 250, Easing.SinInOut);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private async void EventsLabelTapped(object sender, TappedEventArgs e)
    {
        try
        {
            ProfileLabel.FontAttributes = FontAttributes.None;
            EventsLabel.FontAttributes = FontAttributes.Bold;
            EventsLayout.TranslateTo(ProfileLayout.X, 0, 250, Easing.SinInOut);
            await ProfileLayout.TranslateTo(-DeviceDisplay.Current.MainDisplayInfo.Width, 0, 250, Easing.SinInOut);
        } 
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}