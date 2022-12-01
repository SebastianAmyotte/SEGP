//Author: Choi Woo Sik
//Reviewer : Choi Woo Sik

//using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Collections.ObjectModel;


namespace SEGP.Pages;

public partial class Checklist : ContentPage
{

    Dictionary<DateTime, ChecklistEntry> checklistEntries;
    DateTime currentDay = DateTime.Today;
    ChecklistEntry currentDisplayedEntry;
    public Checklist()
	{
		InitializeComponent();
        DatesLBL.Text = currentDay.ToShortDateString();
        checklistEntries = new Dictionary<DateTime, ChecklistEntry>();
        UpdateCurrentEntry();
    }


    void UpdateCurrentEntry()
    {
        if (checklistEntries.ContainsKey(currentDay))
        {
            currentDisplayedEntry = checklistEntries[currentDay];
        }
        else
        {
            currentDisplayedEntry = new ChecklistEntry("", currentDay);
            checklistEntries.Add(currentDay, currentDisplayedEntry);
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
        currentDay = currentDay.AddDays(1);
        DatesLBL.Text = currentDay.ToShortDateString();
        UpdateCurrentEntry();

    }

    void OnDeleteButtonPress(object sender, EventArgs e)
    {
        DisplayAlert("Deleted item", "Item deleted! (Not actually)", "OK");
    }

    async void OnAddButtonPress(object sender, EventArgs e)
    {
        String addedList = await DisplayPromptAsync("Todo", "What's the task?", "OK");
        currentDisplayedEntry.SetEntry(addedList);
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

    public String SerializeToDB()
    {
        return JsonSerializer.Serialize(checklistEntries);
    }

    public void DeserializeFromDB()
    {
        //checklistEntries = JsonSerializer.Deserialize<Dictionary<DateTime, ChecklistEntry>>(json);
        //FIXME
    }

}

public class ChecklistEntry
{

    public String listEntry;
    public DateTime dates;

    public ChecklistEntry(String entry, DateTime dates)
    {
        this.listEntry = entry;
        this.dates = dates;
    }

 
    public String GetEntry()
    {
        return listEntry;
    }


    public void SetEntry(String listEntry)
    {
        this.listEntry = listEntry;
    }


}