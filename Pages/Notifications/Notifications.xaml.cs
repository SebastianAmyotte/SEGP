namespace SEGP7.Pages;

public partial class NotificationsPage : ContentPage
{
	public NotificationsPage()
	{
		InitializeComponent();
	}

    private void OnDeleteButtonPressed(object sender, EventArgs e)
    {
        DisplayAlert("Deleted item", "Item deleted! (Not actually)", "OK");
    }

    private void OnAddButtonPressed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddNotificationPage());
        
    }
}