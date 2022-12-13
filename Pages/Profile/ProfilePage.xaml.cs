namespace SEGP7.Pages;

using global::Firebase.Auth;
using SEGP7.Authentication;
using SEGP7.Firebase;

public partial class ProfilePage : ContentPage
{
    FirebaseAuthLink currentCredentials;
	public ProfilePage()
	{
        MessagingCenter.Subscribe<FirebaseAuthLink>(this, "GetCredentials", (newCredentials) =>
        {
            currentCredentials = newCredentials;
        });
        MessagingCenter.Send("", "SendCredentials");
        InitializeComponent();
        Username.Text = currentCredentials.User.Email;
    }

    public void BackupAndRestoreButtonPresssed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new BackupAndRestorePage());
    }

    public void OnLogoutButtonPressed(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }

    public async void OnChangePasswordButtonPressed(object sender, EventArgs e)
    {
        String userEmail = currentCredentials.User.Email;
        bool result = await DisplayAlert("Change Password?", $"If you would like a new password, we can send an email to {userEmail} containing a link to create a new password. Continue?", "Yes", "No");
        if (result)
        {
            MessagingCenter.Send(userEmail, "ResetPassword");
            DisplayAlert("Password reset", $"An email has been sent to {userEmail} with a link to change your password. Check your junk/spam!", "OK");
        }
    }
    public void OnDeleteAccountButtonPressed(object sender, EventArgs e)
    {

    }
    public void OnQuitButtonPressed(object sender, EventArgs e)
    {
        //Quit the app
        Application.Current.Quit();
    }
}