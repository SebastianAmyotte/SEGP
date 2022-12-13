using CommunityToolkit.Mvvm.Messaging;
using Firebase.Auth;
using Newtonsoft.Json;
using SEGP7.Tools;
using System.Text;

namespace SEGP7.Pages;

public partial class JournalPage : ContentPage
{
    Dictionary<DateTime, JournalEntry> dailyEntries;
    DateTime currentDay = DateTime.Today;
    JournalEntry currentDisplayedEntry;

    public JournalPage()
    {
        dailyEntries = new Dictionary<DateTime, JournalEntry>();
        
        InitializeComponent();
        UpdateCurrentEntry();
        datePicker.Date = currentDay;
        datePicker.MaximumDate = currentDay;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            LoadFromDisk();
        }
        catch (Exception e)
        {

        }
        UpdateCurrentEntry();
    }

        void OnBackButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(-1);
        datePicker.Date = currentDay;
        LookForUnsavedWorkAsync(currentDisplayedEntry, (String)ratingPicker.SelectedItem, dailyThoughts.Text); //Await Async
        UpdateCurrentEntry();
    }

    void OnDatePickerChanged(object sender, EventArgs e)
    {
        DatePicker datePicker = (DatePicker)sender;
        currentDay = datePicker.Date;
        LookForUnsavedWorkAsync(currentDisplayedEntry, (String)ratingPicker.SelectedItem, dailyThoughts.Text);
        UpdateCurrentEntry();
    }

    void OnNextButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(1);
        if (currentDay > DateTime.Now)
        {
            DisplayAlert("Error", "Tomorrow hasn't started yet!", "OK");
            currentDay = currentDay.AddDays(-1);
        }
        else
        {
            datePicker.Date = currentDay;
            LookForUnsavedWorkAsync(currentDisplayedEntry, (String)ratingPicker.SelectedItem, dailyThoughts.Text); //Await Async
            UpdateCurrentEntry();
        }

    }

    void UpdateCurrentEntry()
    {
        //Update data structure
        if (dailyEntries.ContainsKey(currentDay))
        {
            currentDisplayedEntry = dailyEntries[currentDay];
        }
        else
        {
            currentDisplayedEntry = new JournalEntry("", "");
            dailyEntries.Add(currentDay, currentDisplayedEntry);
        }
        //Update UI
        dailyThoughts.Text = currentDisplayedEntry.GetEntry();
        ratingPicker.SelectedItem = currentDisplayedEntry.GetRating();
    }

    async void LookForUnsavedWorkAsync(JournalEntry currentJournal, String rating, String dailyThoughts)
    {
        if (ChangesMade(currentJournal, rating, dailyThoughts))
        {
            bool response = await DisplayAlert("Unsaved work", "Unsaved work will be lost, would you like to save now?", "Save", "Discard");
            if (response)
            {
                currentJournal.SaveRating(rating);
                currentJournal.SaveEntry(dailyThoughts);
            }
        }
    }

    bool ChangesMade(JournalEntry currentJournal, String rating, String dailyThoughts)
    {
        bool changesMadeToRating = !currentJournal.GetRating().Equals(rating);
        bool changesMadeToJournalEntry = !currentJournal.GetEntry().Equals(dailyThoughts);
        return changesMadeToRating || changesMadeToJournalEntry;
    }
    void OnSaveButtonPressed(object sender, EventArgs e)
    {
        currentDisplayedEntry.SaveEntry(dailyThoughts.Text);
        currentDisplayedEntry.SaveRating((String)ratingPicker.SelectedItem);
        SaveToDisk();
        DisplayAlert("Saved", "Journal entry saved", "OK");
    }

    async void OnRevertButtonPressed(object sender, EventArgs e)
    {
        bool response = await DisplayAlert("Discard?", "Are you sure you want to discard your changes?", "Discard", "Keep");
        if (response)
        {
            ratingPicker.SelectedItem = currentDisplayedEntry.GetRating();
            dailyThoughts.Text = currentDisplayedEntry.GetEntry();
        }
    }

    void SaveToDisk()
    {
        DiskIO diskIO = new DiskIO("journal.txt");
        diskIO.WriteToFile(new Serializer().Serialize(dailyEntries));
    }
    void LoadFromDisk()
    {
        DiskIO diskIO = new DiskIO("journal.txt");
        String result = diskIO.ReadFromFile();
        dailyEntries = JsonConvert.DeserializeObject<Dictionary<DateTime, JournalEntry>>(result);
    }
}