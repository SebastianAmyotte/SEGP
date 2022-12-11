namespace SEGP7;
using SEGP7.Authentication;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		App.Current.MainPage = new NavigationPage(new LoginPage());
	}
}
