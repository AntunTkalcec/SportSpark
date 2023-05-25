using SportSpark.ViewModels;

namespace SportSpark.Views.ContentViews;

public partial class EventVisibleToModal : ContentPage
{
	private readonly EventVisibleToViewModel _viewModel;
	public EventVisibleToModal(EventVisibleToViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_viewModel = vm;
	}
}