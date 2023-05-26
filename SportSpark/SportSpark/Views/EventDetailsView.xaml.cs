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
}