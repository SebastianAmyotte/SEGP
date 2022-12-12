using Firebase.Auth;
using SEGP7.Pages;
using SEGP7.Firebase;
namespace SEGP7;

public partial class TabbedNavigation : Shell
{
    public BitIODatabaseController firebaseDB;
    public TabbedNavigation()
	{
        firebaseDB = new BitIODatabaseController();
        InitializeComponent();
    }
}