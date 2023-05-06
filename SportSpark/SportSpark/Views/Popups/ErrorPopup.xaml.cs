using CommunityToolkit.Maui.Views;

namespace SportSpark.Views.Popups;

public partial class ErrorPopup : Popup
{
	public string ErrorMsg { get; set; }
	public ErrorPopup(string errorMsg)
	{
		InitializeComponent();
		ErrorMsg = errorMsg;
		BindingContext = this;
	}

    private void TryAgain(object sender, EventArgs e)
    {
		Close(true);
    }

    private void Ok(object sender, EventArgs e)
    {
		Close(false);
    }
}