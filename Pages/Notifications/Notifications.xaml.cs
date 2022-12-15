using Newtonsoft.Json;
using System.Collections.ObjectModel;
using SEGP7.Tools;

namespace SEGP7.Pages;

// Secondary Author: Sebastian Amyotte - Wrote the necessary code for data persistence

public partial class NotificationsPage : ContentPage
{
    ObservableCollection<NotificationsEntry> notificationEntries;
    bool pageLoaded = false;
	public NotificationsPage()
	{
		InitializeComponent();
        //create new list of notifications
        notificationEntries = new ObservableCollection<NotificationsEntry>();
		//Binds notification list to xaml
		NotificationsList.ItemsSource= notificationEntries;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            if (!pageLoaded)
            {
                LoadFromDisk();
                pageLoaded = true;
            }
            else
            {
                SaveToDisk();
            }
        }
        catch (Exception error) {
            Console.WriteLine(error);
        }
    }

    protected override void OnDisappearing()
    {
        base.OnAppearing();
        pageLoaded = false;
    }

    async void OnDeleteButtonPressed(object sender, EventArgs e)
    {
		//Make sure somethingis actually selected
		if (NotificationsList.SelectedItem != null)
		{
			//Get the currently selected entry
			NotificationsEntry tappedEntry = (NotificationsEntry)NotificationsList.SelectedItem;
			bool answer = await DisplayAlert("Confirm delete:", $"Are you sure you want to remove '{tappedEntry.NotificationTitle}'?", "Yes", "No");
			if (answer)
			{
				notificationEntries.Remove(tappedEntry);
			}
            ChangesMade();
        }
		else
		{
			await DisplayAlert("Notice", $"No item selected to delete", "Ok");
		}
	}

    private void OnAddButtonPressed(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddNotificationPage(notificationEntries));
        pageLoaded = true;
    }

    // Data persistence - Sebastian Amyotte
    void ChangesMade()
    {
        SaveToDisk();
    }

    void SaveToDisk()
    {
        DiskIO diskIO = new DiskIO("notifications.txt");
        String xml = ObjectSerializer<ObservableCollection<NotificationsEntry>>.ToXml(notificationEntries);
        diskIO.WriteToFile(xml);
    }
    void LoadFromDisk()
    {
        try
        {
            DiskIO diskIO = new DiskIO("notifications.txt");
            String dataFromDisk = diskIO.ReadFromFile();
            notificationEntries = ObjectSerializer<ObservableCollection<NotificationsEntry>>.FromXml(dataFromDisk);
            NotificationsList.ItemsSource = notificationEntries;
        }
        catch (Exception e) {
            //File not found
            DiskIO diskIO = new DiskIO("notifications.txt");
            diskIO.WriteToFile("");
        }
    }
}