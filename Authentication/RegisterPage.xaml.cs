using SEGP7.ViewModels;
namespace SEGP7.Authentication;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel(Navigation);
    }

    /**
    private void RegisterUserButtonPushed(object sender, EventArgs e)
    {
        //TODO
    }*/

    private void CancelRegisterButtonPushed(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}