using CommunityToolkit.Mvvm.Messaging;
using Firebase.Auth;
using Microsoft.Maui.Platform;
using SEGP7.Firebase;

namespace SEGP7.Authentication;

public partial class LoginPage : ContentPage
{
    FirebaseAuthenticationController authController = new FirebaseAuthenticationController(); 
    public LoginPage()
    {
        InitializeComponent();
    }

    private void LoginUserButtonPushed(object sender, EventArgs e)
    {
        authController.LoginUser(UserEmail.Text, UserPassword.Text);
        PushLoggedInPage();
    }

    private void RegisterUserButtonPushed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage());
    }

    private void ForgotPasswordButtonPushed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ResetPasswordPage());
    }

    private void QuitButtonPushed(object sender, EventArgs e)
    {
        //Quit the app
        Application.Current.Quit();
    }

    private void PushLoggedInPage()
    {
        Application.Current.MainPage = new TabbedNavigation();
    }
}