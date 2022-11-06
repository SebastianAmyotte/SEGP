namespace SEGP.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register());
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profile());
    }

    void OnForgotPasswordClicked(object sender, EventArgs e)
    {

    }
}