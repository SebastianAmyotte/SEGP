namespace SEGP.Pages;

public partial class Journal : ContentPage
{
    Dictionary<DateTime, JournalEntry> dailyEntries;
    DateTime currentDay = DateTime.Today;
    JournalEntry currentDisplayedEntry;

    public Journal()
    {
        InitializeComponent();
        //TODO: Serialize dailyEntries, for now create blank entry
        dailyEntries = new Dictionary<DateTime, JournalEntry>();
        UpdateCurrentEntry();
    }

    void OnBackButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(-1);
        LookForUnsavedWorkAsync(currentDisplayedEntry, rating.Text, dailyThoughts.Text); //Await Async
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
        date.Text = currentDay.ToShortDateString();
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

    void SerializeToDisk()
    {
        //TODO
    }

    void LoadSerializedFromDisk()
    {
        //TODO
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