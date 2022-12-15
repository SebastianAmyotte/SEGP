namespace SEGP7.Pages;

using Newtonsoft.Json;
using Tools;

public partial class BackupAndRestorePage : ContentPage
{
	public BackupAndRestorePage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        String lastBackedUpDate;
        try
        {
            String readFromFile = new DiskIO("lastbackup.txt").ReadFromFile();
            DateTime lastBackedUpDateTime = JsonConvert.DeserializeObject<DateTime>(readFromFile);
            lastBackedUpDate = $"{lastBackedUpDateTime.ToShortDateString()} at {DateTime.Now.ToShortTimeString()}";
        } catch (Exception e)
        {
            lastBackedUpDate = "Never";
        }
        LastBackupLabel.Text = $"Last backup completed: {lastBackedUpDate}";
    }

    public void BackupNowButtonPressed(object sender, EventArgs e)
    {
        //Backup journal page
        String fileContents = new DiskIO("journal.txt").ReadFromFile();
        //Replace all single quotes with double quotes, to escape them for sql execution
        fileContents = fileContents.Replace("'", "''");
        MessagingCenter.Send(fileContents, "SaveJournal");

        //Backup Notifications page
        fileContents = new DiskIO("notifications.txt").ReadFromFile();
        //Replace all single quotes with double quotes, to escape them for sql execution
        fileContents = fileContents.Replace("'", "''");
        MessagingCenter.Send(fileContents, "SaveNotifications");

        //Save current time the backup completed
        new DiskIO("lastbackup.txt").WriteToFile(new Serializer().Serialize(DateTime.Now));
        LastBackupLabel.Text = $"Last backup completed: {DateTime.Now.ToShortDateString()} at {DateTime.Now.ToShortTimeString()}";
        //Back up the data to the cloud
        DisplayAlert("Complete", "Backup completed", "OK");
    }
    public void RestoreNowButtonPressed(object sender, EventArgs e)
    {
        //Journal
        MessagingCenter.Subscribe<String>(this, "SendLoadedJournal", (EntryObject) =>
        {
            new DiskIO("journal.txt").WriteToFile(EntryObject);
        });
        MessagingCenter.Send("LoadJournal", "LoadJournal");

        //Notifications
        MessagingCenter.Subscribe<String>(this, "SendLoadedNotifications", (EntryObject) =>
        {
            new DiskIO("notifications.txt").WriteToFile(EntryObject);
        });
        MessagingCenter.Send("LoadNotifications", "LoadNotifications");

        DisplayAlert("Complete", "Restore completed", "OK");
        //End Journal
    }

    public void GoBackButtonPressed(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}