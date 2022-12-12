using System.Collections.ObjectModel;

namespace SEGP7.Pages;

public partial class NotificationsPage : ContentPage
{
    ObservableCollection<NotificationsEntry> notificationEntries;
	public NotificationsPage()
	{
		InitializeComponent();
        //create new list of notifications
        notificationEntries= new ObservableCollection<NotificationsEntry>();
		//Binds notification list to xaml
		NotificationsList.ItemsSource= notificationEntries;
	}

    async void OnDeleteButtonPressed(object sender, EventArgs e)
    {
		//Make sure somethingis actually selected
		if (NotificationsList.SelectedItem != null)
		{
			//Get the currently selected entry
			NotificationsEntry tappedEntry = (NotificationsEntry)NotificationsList.SelectedItem;
			bool answer = await DisplayAlert("Confirm delete:", $"Are you sure you want to remove '{tappedEntry.NotificationName}'?", "Yes", "No");
			if (answer)
			{
				notificationEntries.Remove(tappedEntry);
			}
		}
		else
		{
			await DisplayAlert("Notice", $"No item selected to delete", "Ok");
		}
	}

    private void OnAddButtonPressed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddNotificationPage(notificationEntries));
    }
}