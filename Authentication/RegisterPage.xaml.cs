namespace SEGP7.Authentication;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private void RegisterUserButtonPushed(object sender, EventArgs e)
    {
        //TODO
    }

    private void CancelRegisterButtonPushed(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}