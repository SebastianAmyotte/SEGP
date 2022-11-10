namespace SEGP.Pages;

public partial class Checklist : ContentPage
{

    Dictionary<DateTime, ChecklistEntry> dailyEntries;
    DateTime currentDay = DateTime.Today;
    ChecklistEntry currentDisplayedEntry;
    public Checklist()
	{
		InitializeComponent();
        DatesLBL.Text = currentDay.ToShortDateString();
        dailyEntries = new Dictionary<DateTime, ChecklistEntry>();
        UpdateCurrentEntry();
    }


    void UpdateCurrentEntry()

    {
        if (dailyEntries.ContainsKey(currentDay))
        {
            currentDisplayedEntry = dailyEntries[currentDay];
        }
        else
        {
            currentDisplayedEntry = new ChecklistEntry("", "", currentDay);
            dailyEntries.Add(currentDay, currentDisplayedEntry);
        }

        currentDisplayedEntry.dates = currentDay;
        currentDisplayedEntry.listEntry = currentDisplayedEntry.GetEntry();

    }
    void OnBackButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(-1);
        DatesLBL.Text = currentDay.ToShortDateString();
        UpdateCurrentEntry();
    }

    void OnNextButtonPressed(object sender, EventArgs e)
    {

        if (currentDay.AddDays(1) > DateTime.Now)
        {
            DisplayAlert("Error", "Tomorrow hasn't started yet!", "OK");
          
        }
        else
        {
            currentDay = currentDay.AddDays(1);
            DatesLBL.Text = currentDay.ToShortDateString();
            UpdateCurrentEntry();
        }

    }

    void OnDeleteButtonPress(object sender, EventArgs e)
    {
        DisplayAlert("Deleted item", "Item deleted! (Not actually)", "OK");
    }

    private void HandleCheck(object sender, EventArgs e)
    {
        CheckBox cb = sender as CheckBox;
    }
    private void HandleUnchecked(object sender, EventArgs e)
    {
        CheckBox cb = sender as CheckBox;

   
    }
    private void HandleThirdState(object sender, EventArgs e)
    {
        CheckBox cb = sender as CheckBox;
      
    }
} 
 
public class ChecklistEntry
{
    public String listEntry;
    public DateTime dates;

    public ChecklistEntry(String entry, String rating, DateTime dates)
    {
        this.listEntry = entry;
        this.dates = dates;
    }

    public String GetEntry()
    {
        return listEntry;
    }

    public void SaveEntry(String entry)
    {
        this.listEntry = entry;
    }


}