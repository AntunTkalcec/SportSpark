using CommunityToolkit.Maui.Views;

namespace SportSpark.Views.Popups;

public partial class RateUserPopup : Popup
{
	public RateUserPopup()
	{
		InitializeComponent();
		stepperValue.Text = rateStepper.Value.ToString();
	}

    private void rateStepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
		stepperValue.Text = e.NewValue.ToString();
    }

	private void Ok(object sender, EventArgs e)
	{
		Close((int)rateStepper.Value);
	}
}