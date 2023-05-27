using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class EventDetailsView : ContentPage
{
	private readonly EventDetailsViewModel _viewModel;
	public EventDetailsView(EventDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_viewModel = vm;
	}


    private async void loadMapBtn_Clicked(object sender, EventArgs e)
    {
		loadMapBtn.IsVisible = false;
		map.IsVisible = true;
        Location location = new((double)_viewModel.EventValue.Lat, (double)_viewModel.EventValue.Long);

        map.Pins.Add(new Pin
		{
			Label = _viewModel.EventValue.Title,
            Location = location
		});
		await Task.Delay(500);
		map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(1)));
    }
}