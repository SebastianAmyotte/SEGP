using Newtonsoft.Json;
using System.Collections.ObjectModel;
using SEGP7.Tools;

namespace SEGP7.Pages;

public partial class NotificationsPage : ContentPage
{
    ObservableCollection<NotificationsEntry> notificationEntries;
    List<NotificationsEntry> notificationEntriesStorage;
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
    }

    //Data persistence
    void ChangesMade()
    {
        SaveToDisk();
    }

    void SaveToDisk()
    {
        DiskIO diskIO = new DiskIO("checklist.txt");
        String xml = ObjectSerializer<ObservableCollection<NotificationsEntry>>.ToXml(notificationEntries);
        diskIO.WriteToFile(xml);
    }
    void LoadFromDisk()
    {
        DiskIO diskIO = new DiskIO("checklist.txt");
        String dataFromDisk = diskIO.ReadFromFile();
        notificationEntries = ObjectSerializer<ObservableCollection<NotificationsEntry>>.FromXml(dataFromDisk);
        NotificationsList.ItemsSource = notificationEntries;
    }
}