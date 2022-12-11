namespace SEGP7.Authentication;

public partial class ResetPasswordPage : ContentPage
{
	public ResetPasswordPage()
	{
		InitializeComponent();
	}
    private void ResetPasswordButtonPushed(object sender, EventArgs e)
    {
        //TODO
    }

    private void CancelResetPasswordButtonPushed(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}