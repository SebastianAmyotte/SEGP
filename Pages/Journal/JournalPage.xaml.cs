using CommunityToolkit.Mvvm.Messaging;
using Firebase.Auth;
using Newtonsoft.Json;
using SEGP7.Tools;
using System.Text;

namespace SEGP7.Pages;

// Author: Sebastian Amyotte
// Description: The main journal page. 

public partial class JournalPage : ContentPage
{
    // Each day (DateTime) is a key that lookups the corresponding journal entry
    Dictionary<DateTime, JournalEntry> dailyEntries;
    // By default, page opens to today's date
    DateTime currentDay = DateTime.Today;
    // The entry that is currently being edited
    JournalEntry currentDisplayedEntry;

    public JournalPage()
    {
        dailyEntries = new Dictionary<DateTime, JournalEntry>();
        InitializeComponent();
        UpdateCurrentEntry();
        datePicker.Date = currentDay;
        // Prevent people from using the picker to go forwards in time
        datePicker.MaximumDate = currentDay;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            // Load the serialized dictionary from disk
            LoadFromDisk();
        } catch (Exception e) { }
        // Update the current entry with the disk data
        UpdateCurrentEntry();
    }

    // This runs when the user navigates away from the Journal page
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Checks for unsaved changes
        LookForUnsavedWorkAsync(currentDisplayedEntry, (String)ratingPicker.SelectedItem ?? "", dailyThoughts.Text);
    }
    
    void OnBackButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(-1);
        datePicker.Date = currentDay;
        LookForUnsavedWorkAsync(currentDisplayedEntry, (String)ratingPicker.SelectedItem ?? "", dailyThoughts.Text); //Await Async
        UpdateCurrentEntry();
    }

    void OnDatePickerChanged(object sender, EventArgs e)
    {
        DatePicker datePicker = (DatePicker)sender;
        currentDay = datePicker.Date;
        LookForUnsavedWorkAsync(currentDisplayedEntry, (String)ratingPicker.SelectedItem ?? "", dailyThoughts.Text);
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
            LookForUnsavedWorkAsync(currentDisplayedEntry, (String)ratingPicker.SelectedItem ?? "", dailyThoughts.Text); //Await Async
            UpdateCurrentEntry();
        }

    }

    
    void UpdateCurrentEntry()
    {
        // Checks to see the new date that was navigated to exists in the dictionary
        if (dailyEntries.ContainsKey(currentDay))
        {
            // If it does, simply load it
            currentDisplayedEntry = dailyEntries[currentDay];
        }
        else
        {
            // If it doesn't, create a new, blank entry, and display that
            currentDisplayedEntry = new JournalEntry("", "");
            dailyEntries.Add(currentDay, currentDisplayedEntry);
        }
        // Update UI
        dailyThoughts.Text = currentDisplayedEntry.GetEntry();
        ratingPicker.SelectedItem = currentDisplayedEntry.GetRating();
    }

    // This looks for unsaved work, and asks the user if they want to save it
    async void LookForUnsavedWorkAsync(JournalEntry currentJournal, String rating, String dailyThoughts)
    {
        if (ChangesMade(currentJournal, rating, dailyThoughts))
        {
            bool response = await DisplayAlert("Unsaved work", "Unsaved work will be lost, would you like to save now?", "Save", "Discard");
            if (response)
            {
                currentJournal.SaveRating(rating);
                currentJournal.SaveEntry(dailyThoughts);
                SaveToDisk();
            }
        }
    }

    // This checks to see if any changes have been made to the current entry
    bool ChangesMade(JournalEntry currentJournal, String rating, String dailyThoughts)
    {
        bool changesMadeToRating = !currentJournal.GetRating().Equals(rating);
        bool changesMadeToJournalEntry = !currentJournal.GetEntry().Equals(dailyThoughts);
        return changesMadeToRating || changesMadeToJournalEntry;
    }

    // Saves the dictionary to disk
    void OnSaveButtonPressed(object sender, EventArgs e)
    {
        currentDisplayedEntry.SaveEntry(dailyThoughts.Text);
        currentDisplayedEntry.SaveRating((String)ratingPicker.SelectedItem);
        SaveToDisk();
        DisplayAlert("Saved", "Journal entry saved", "OK");
    }
     
    // Reverts all changes since last save
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
        try
        {
            DiskIO diskIO = new DiskIO("journal.txt");
            String dataFromDisk = diskIO.ReadFromFile();
            dailyEntries = JsonConvert.DeserializeObject<Dictionary<DateTime, JournalEntry>>(dataFromDisk);
        } catch (Exception e)
        {
            //File not found
            DiskIO diskIO = new DiskIO("journal.txt");
            diskIO.WriteToFile("");
        }
    }
}