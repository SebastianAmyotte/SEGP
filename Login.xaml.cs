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
        //Close the keyboard
        userpassword.Unfocus();
    }
}