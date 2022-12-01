
using SEGP.ViewModels;

namespace SEGP.Pages;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel(Navigation);
    }

    void RegisterUserClicked(object sender, EventArgs e)
    {
        //registers a new user into database
    }

    async void CancelRegistrationClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}