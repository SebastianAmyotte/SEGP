namespace SEGP.Pages;

public partial class Journal : ContentPage
{
    List<JournalEntry> dailyEntries;

	public Journal()
	{
        InitializeComponent();
    }

	void OnBackButtonPressed(object sender, EventArgs e)
	{
        //Change the page a day back
        DisplayAlert("Error", "No previous day found", "OK");
	}
    void OnNextButtonPressed(object sender, EventArgs e)
    {
        //Change the page a day forward
        DisplayAlert("Error", "No next day found", "OK");
    }

    void OnSaveButtonPressed(object sender, EventArgs e)
    {
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
    String date;

    JournalEntry(String entry, String rating, string date)
    {
        this.entry = entry;
        this.rating = rating;
        this.date = date;
    }

    String GetEntry()
    {
        return entry;
    }

    String GetRating()
    {
        return rating;
    }
}