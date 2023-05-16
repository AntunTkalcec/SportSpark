using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class CreateEventView : ContentPage
{
	private readonly CreateEventViewModel _viewModel;

	public CreateEventView(CreateEventViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_viewModel = vm;
    }

    private void title_Completed(object sender, EventArgs e)
    {
        description.Focus();
    }

    private void Editor_Completed(object sender, EventArgs e)
    {
        duration.Focus();
    }

    private void duration_Completed(object sender, EventArgs e)
    {
        price.Focus();
    }

    private void price_Completed(object sender, EventArgs e)
    {
        participants.Focus();
    }
}