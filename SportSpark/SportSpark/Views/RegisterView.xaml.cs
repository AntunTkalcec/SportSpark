using SportSpark.Helpers;
using SportSpark.ViewModels;

namespace SportSpark.Views;

public partial class RegisterView : ContentPage
{
    private RegisterViewModel vm;
	public RegisterView(RegisterViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        vm = viewModel;
	}

    private void CheckFirstName()
    {
        if (vm.CheckFirstName())
        {
            firstNameBorder.Stroke = Color.FromArgb(ColorHelper.SportSparkLightGreen);
        }
        else
        {
            firstNameBorder.Stroke = Colors.Red;
        }
    }

    private void CheckLastName()
    {
        if (vm.CheckLastName())
        {
            lastNameBorder.Stroke = Color.FromArgb(ColorHelper.SportSparkLightGreen);
        }
        else
        {
            lastNameBorder.Stroke = Colors.Red;
        }
    }

    private void CheckUsername()
    {
        if (vm.CheckUserName())
        {
            usernameBorder.Stroke = Color.FromArgb(ColorHelper.SportSparkLightGreen);
        }
        else
        {
            usernameBorder.Stroke = Colors.Red;
        }
    }

    private void CheckEmail()
    {
        if (vm.CheckEmail())
        {
            emailBorder.Stroke = Color.FromArgb(ColorHelper.SportSparkLightGreen);
        }
        else
        {
            emailBorder.Stroke = Colors.Red;
        }
    }

    private void CheckPassword()
    {
        if (vm.CheckPassword())
        {
            passwordBorder.Stroke = Color.FromArgb(ColorHelper.SportSparkLightGreen);
        }
        else
        {
            passwordBorder.Stroke = Colors.Red;
        }
    }
    private void FirstName_Completed(object sender, EventArgs e)
    {
        CheckFirstName();
        lastName.Focus();
    }

    private void FirstName_Unfocused(object sender, EventArgs e)
    {
        CheckFirstName();
    }

    private void LastName_Completed(object sender, EventArgs e)
    {
        CheckLastName();
        username.Focus();
    }

    private void LastName_Unfocused(object sender, EventArgs e)
    {
        CheckLastName();
    }

    private void Username_Completed(object sender, EventArgs e)
    {
        CheckUsername();
        email.Focus();
    }

    private void Username_Unfocused(object sender, EventArgs e)
    {
        CheckUsername();
    }

    private void Email_Completed(object sender, EventArgs e)
    {
        CheckEmail();
        password.Focus();
    }
    private void Email_Unfocused(object sender, EventArgs e)
    {
        CheckEmail();
    }

    private async void Password_Completed(object sender, EventArgs e)
    {
        CheckPassword();
    }
    private void Password_Unfocused(object sender, EventArgs e)
    {
        CheckPassword();
    }
}