using SportSpark.Helpers;
using SportSpark.Services;
using SportSpark.Views;
using System.Diagnostics;
using System.Globalization;

namespace SportSpark;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        SetCulture();

        if (!(Preferences.Get("welcomed", "0") == "1"))
        {
            MainPage = new NavigationPage(new FirstStartupView());
        }
        else
        {
            MainPage = new AppShell();
        }
    }

    private static async void SetCulture()
    {
        try
        {
            CultureInfo culture;
            string chosenCulture = Preferences.Get("Language", "en-US");

            if (chosenCulture == "en-US")
            {
                culture = new("en-US");
            }
            else
            {
                culture = new("hr-HR");
            }
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            LanguageHelper.Instance.SetCultureInfo(culture);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error working with cultures.");
        }
    }
}