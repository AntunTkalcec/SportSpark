using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Diagnostics;
using System.Windows.Input;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace SportSpark.Views.Popups;

public partial class LocationSelectionPopup : Popup
{
    public ICommand GetCurrentLocation { get; }
    public LocationSelectionPopup()
	{
		InitializeComponent();
        GetCurrentLocation = new AsyncRelayCommand(GetCurrentLocationAsync);
        GetCurrentLocation.Execute(null);
    }

    private async Task GetCurrentLocationAsync()
    {
        try
        {
            GeolocationRequest request = new(GeolocationAccuracy.Best, TimeSpan.FromSeconds(60));

            Location location = await Geolocation.Default.GetLocationAsync(request);

            if (location != null)
            {
                Map map = new(new MapSpan(location, 0.01, 0.01));
                map.Pins.Add(new Pin
                {
                    Label = "You",
                    Location = location
                });
                map.MapClicked += OnMapClicked;
                if (map.VisibleRegion != null)
                {
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(0.4)));
                }
                mainGrid.Add(map);
                mainGrid.SetColumnSpan(map, 2);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Toast.Make("Couldn't get your location").Show();
        }
    }

    void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        Close(new List<double> { e.Location.Latitude, e.Location.Longitude });
    }
}