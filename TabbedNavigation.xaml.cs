using Firebase.Auth;
using SEGP7.Pages;
namespace SEGP7;

public partial class TabbedNavigation : Shell
{
    public FirebaseAuthLink userCredentials;

    public TabbedNavigation(FirebaseAuthLink userCredentials)
	{
        InitializeComponent();
    }
}