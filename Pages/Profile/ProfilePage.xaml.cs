namespace SEGP7.Pages;
using SEGP7.Authentication;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
	}

    public void OnLogoutButtonPressed(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }

    public void OnChangePasswordButtonPressed(object sender, EventArgs e)
    {
    }
    public void OnDeleteAccountButtonPressed(object sender, EventArgs e)
    {

    }
}