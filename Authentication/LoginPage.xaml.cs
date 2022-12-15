using CommunityToolkit.Mvvm.Messaging;
using Firebase.Auth;
using Microsoft.Maui.Platform;
using SEGP7.Firebase;
using SEGP7.Tools;

namespace SEGP7.Authentication;

public partial class LoginPage : ContentPage
{
    FirebaseAuthenticationController authController = new FirebaseAuthenticationController();
    BitIODatabaseController firebaseDB = new BitIODatabaseController();
    String previouslyLoggedIntoAccount;
    public LoginPage()
    {
        InitializeComponent();
        try
        {
            previouslyLoggedIntoAccount = new DiskIO("LastUserEmail.txt").ReadFromFile();
        } catch (Exception exception)
        {
            previouslyLoggedIntoAccount = "";
        }
    }
    
    private void LoginUserButtonPushed(object sender, EventArgs e)
    {
        try
        {
            if (authController.LoginUser(UserEmail.Text?.ToLower() ?? "", UserPassword.Text ?? ""))
            {

                if (UserEmail.Text.ToLower() != previouslyLoggedIntoAccount)
                {
                    new DiskIO("LastUserEmail.txt").WriteToFile(UserEmail.Text.ToLower());
                    if (previouslyLoggedIntoAccount != "")
                    {
                        DisplayAlert("Notice", "You have logged into a new account, if you would like to restore the data from this new account, go to 'Backup and Restore' section of the profile page.", "OK");
                    }
                }
                Application.Current.MainPage = new TabbedNavigation();
            }
        }
        catch (Exception error)
        {
            String errorReason = error.Message.Split("Reason:")[1];
            errorReason = errorReason.Remove(errorReason.Length - 1);
            DisplayAlert("Error logging in", errorReason, "OK");
        }
    }

    private void RegisterUserButtonPushed(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new RegisterPage(authController));
        } catch (Exception error)
        {
            DisplayAlert("Error creating account", error.Message, "OK");
        }
    }

    private void ForgotPasswordButtonPushed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ResetPasswordPage(authController));
    }

    private void QuitButtonPushed(object sender, EventArgs e)
    {
        //Quit the app
        Application.Current.Quit();
    }
}