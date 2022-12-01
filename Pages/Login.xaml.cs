//Primary author: Andrew Zarling
//Secondary author: Sebastian Amyotte
//Reviewer: Andrew Zarling
using Firebase.Auth;
using SEGP.ViewModels;

namespace SEGP.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel(Navigation);
	}

    async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register());
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        
        await Navigation.PopAsync();
    }

    void OnForgotPasswordClicked(object sender, EventArgs e)
    {

    }

    async void OnLoginClicked(object sender, EventArgs e)
    {
        var mAuthConfig = new FirebaseConfig("AIzaSyA-NBm1ekoHirJT9Y_bg2mZux9zN9jopBw");
        var mAuth = new FirebaseAuthProvider(mAuthConfig);
        var loginTask = await mAuth.SignInWithEmailAndPasswordAsync(useremail.Text, userpassword.Text);
        DisplayAlert("Logged in!", $"Logged into {loginTask.User.Email}", "Ok");
    }
}