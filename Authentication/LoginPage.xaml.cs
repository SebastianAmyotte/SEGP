namespace SEGP7.Authentication;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void LoginUserButtonPushed(object sender, EventArgs e)
    {
        Application.Current.MainPage = new TabbedNavigation();
    }

    private void RegisterUserButtonPushed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage());
    }

    private void ForgotPasswordButtonPushed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ResetPasswordPage());
    }
}