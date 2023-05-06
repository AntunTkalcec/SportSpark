using CommunityToolkit.Mvvm.ComponentModel;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSpark.Views.ContentViews;

public partial class ProfileContentView : ContentView
{
	public static readonly BindableProperty UserObjectProperty =
		BindableProperty.Create(nameof(UserObject), typeof(UserDTO), typeof(ProfileContentView));

	public UserDTO UserObject
	{
		get { return (UserDTO)GetValue(UserObjectProperty); }
		set { SetValue(UserObjectProperty, value); }
	}

	public ProfileContentView()
	{
		InitializeComponent();
		BindingContext = this;
	}
}