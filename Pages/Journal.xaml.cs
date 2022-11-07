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
        updateCurrentEntry();
    }

	void OnBackButtonPressed(object sender, EventArgs e)
	{
        currentDay = currentDay.AddDays(-1);
        if (changesMade())
        {
            Task<bool> userChoice = DisplayAlert("Changes made", "Unsaved changes, would you like to save?", "Yes", "No");
            if (userChoice.Result)
            {
                OnSaveButtonPressed(null, null);
            }
        }
        updateCurrentEntry();
	}
    void OnNextButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(1);
        if (currentDay > DateTime.Now)
        {
            DisplayAlert("Error", "Tomorrow hasn't started yet!", "OK");
            currentDay = currentDay.AddDays(-1);
        } else
        {
            if (changesMade())
            {
                Task<bool> userChoice = DisplayAlert("Changes made", "Unsaved changes, would you like to save?", "Yes", "No");
                if (userChoice.Result)
                {
                    OnSaveButtonPressed(null, null);
                }
            }
            updateCurrentEntry();
        }
        
    }

    void updateCurrentEntry()
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
        date.Text = currentDay.ToString();
        rating.Text = currentDisplayedEntry.GetRating();
        dailyThoughts.Text = currentDisplayedEntry.GetEntry();
    }

    bool changesMade()
    {
        bool changesMadeToRating = currentDisplayedEntry.GetRating().Equals(rating.Text);
        bool changesMadeToJournalEntry = currentDisplayedEntry.GetEntry().Equals(dailyThoughts.Text);
        return changesMadeToRating || changesMadeToJournalEntry;
    }


    void OnSaveButtonPressed(object sender, EventArgs e)
    {
        currentDisplayedEntry.SaveEntry(dailyThoughts.Text);
        currentDisplayedEntry.SaveRating(rating.Text);
        DisplayAlert("Saved", "Journal entry saved", "OK");
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