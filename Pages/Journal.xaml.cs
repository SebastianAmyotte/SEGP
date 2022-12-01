//Author: Sebastian Amyotte
//Reviewer: Sebastian Amyotte
using System.Text.Json;
namespace SEGP.Pages;

public partial class Journal : ContentPage
{
    Dictionary<DateTime, JournalEntry> dailyEntries;
    DateTime currentDay = DateTime.Today;
    JournalEntry currentDisplayedEntry;

    public Journal()
    {
        InitializeComponent();
        datePicker.Date = currentDay;
        datePicker.MaximumDate = currentDay;
        //TODO: Serialize dailyEntries, for now create blank entry
        dailyEntries = new Dictionary<DateTime, JournalEntry>();
        UpdateCurrentEntry();
    }

    void OnBackButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(-1);
        datePicker.Date = currentDay;
        LookForUnsavedWorkAsync(currentDisplayedEntry, rating.Text, dailyThoughts.Text); //Await Async
        UpdateCurrentEntry();
    }

    void OnDatePickerChanged(object sender, EventArgs e)
    {
        DatePicker datePicker = (DatePicker)sender;
        currentDay = datePicker.Date;
        LookForUnsavedWorkAsync(currentDisplayedEntry, rating.Text, dailyThoughts.Text);
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
            LookForUnsavedWorkAsync(currentDisplayedEntry, rating.Text, dailyThoughts.Text); //Await Async
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
            currentDisplayedEntry = new JournalEntry("", "", currentDay);
            dailyEntries.Add(currentDay, currentDisplayedEntry);
        }
        //Update UI
        rating.Text = currentDisplayedEntry.GetRating();
        dailyThoughts.Text = currentDisplayedEntry.GetEntry();
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
        currentDisplayedEntry.SaveRating(rating.Text);
        DisplayAlert("Saved", "Journal entry saved", "OK");
    }

    async void OnRevertButtonPressed(object sender, EventArgs e)
    {
        bool response = await DisplayAlert("Discard?", "Are you sure you want to discard your changes?", "Discard", "Keep");
        if (response)
        {
            rating.Text = currentDisplayedEntry.GetRating();
            dailyThoughts.Text = currentDisplayedEntry.GetEntry();
        }
    }
    String SerializeToJson()
    {
        return JsonSerializer.Serialize(dailyEntries);
    }

    void LoadSerializedFromJson(String json)
    {
        dailyEntries = JsonSerializer.Deserialize<Dictionary<DateTime, JournalEntry>>(json); 
    }
}

public class JournalEntry
{
    String entry;
    String rating;
    DateTime date;

    public JournalEntry(String entry, String rating, DateTime date)
    {
        this.entry = entry;
        this.rating = rating;
        this.date = date;
    }

    public String GetEntry()
    {
        return entry;
    }

    public void SaveEntry(String entry)
    {
        this.entry = entry;
    }
    public String GetRating()
    {
        return rating;
    }
    public void SaveRating(String rating)
    {
        this.rating = rating;
    }
}