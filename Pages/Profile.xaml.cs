//Primary author: Andrew Zarling
//Reviewer: Andrew Zarling
namespace SEGP.Pages;
public partial class Profile : ContentPage
{
    
	public Profile()
	{
        Notifications notificationsPage = new Notifications();
        InitializeComponent();
	}

    void OnEditImage(object sender, EventArgs e)
    {
        //user can add/change profile image
    }

    async void OnLoginClicked(object sender, EventArgs e)
	{
       await Navigation.PushAsync(new Login());
    }
   async void OnLogoutClicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
    void OnBackupClicked(object sender, EventArgs e)
    {
        DisplayActionSheet("Backup Data?", "Cancel", "Backup");
    }
    void OnRestoreClicked(object sender, EventArgs e)
    {
        DisplayActionSheet("Restore Data?", "Cancel", "Restore");
    }

}