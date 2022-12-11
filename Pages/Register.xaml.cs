//Primary author: Andrew Zarlin
//Reviewer: Andrew Zarlin
using SEGP.ViewModels;

namespace SEGP.Pages;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel(Navigation);
    }

    async void CancelRegistrationClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}