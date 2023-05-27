using CommunityToolkit.Maui.Views;
using SportSpark.Helpers;

namespace SportSpark.Views.Popups;

public partial class RateUserPopup : Popup
{
    public static LanguageHelper Language
    {
        get
        {
            return LanguageHelper.Instance;
        }
    }
    public RateUserPopup()
	{
		InitializeComponent();
		stepperValue.Text = rateStepper.Value.ToString();
        BindingContext = this;
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