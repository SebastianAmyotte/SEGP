namespace SEGP7.Pages;

using global::Firebase.Auth;
using SEGP7.Authentication;

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