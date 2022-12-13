using SEGP7.Firebase;

namespace SEGP7.Authentication;

public partial class ResetPasswordPage : ContentPage
{
    FirebaseAuthenticationController authenticationController;
	public ResetPasswordPage(FirebaseAuthenticationController authenticationController)
	{
        this.authenticationController = authenticationController;
		InitializeComponent();
	}
    private void ResetPasswordButtonPushed(object sender, EventArgs e)
    {
        String emailToReset = emailEntry.Text;
        if (emailToReset != "")
        {
            authenticationController.ResetPassword(emailToReset);
            DisplayAlert("Reset", $"If {emailToReset} is an account, we will send an email containing a link to reset your password. Check your junk/spam!", "OK");
        } else
        {
            DisplayAlert("Error", "Invalid email", "OK");
        }
    }

    private void CancelResetPasswordButtonPushed(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}