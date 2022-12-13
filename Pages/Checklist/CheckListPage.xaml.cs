using System.Collections.ObjectModel;

namespace SEGP7.Pages;

public partial class ChecklistPage : ContentPage
{
    DateTime currentDay = DateTime.Today;
    Dictionary<DateTime, ObservableCollection<ChecklistEntry>> checklistEntries;
    ObservableCollection<ChecklistEntry> todaysChecklistEntries;
    
    public ChecklistPage()
	{
		InitializeComponent();
        //Create a new dictionary of todo items
        checklistEntries = new Dictionary<DateTime, ObservableCollection<ChecklistEntry>>();
        //Create today's checklist
        todaysChecklistEntries = new ObservableCollection<ChecklistEntry>();
        //Add today's checklist to our dictionary of checklists
        checklistEntries.Add(currentDay, todaysChecklistEntries);
        //Bind todays checklist to xaml
        ToDoList.ItemsSource = todaysChecklistEntries;
    }
    
    private void UpdateCurrentChecklist()
    {
        //Check if dictionary of days contains the day that is now selected
        if (checklistEntries.ContainsKey(currentDay))
        {
            //It does, set todays checklist as the current entry list
            todaysChecklistEntries = checklistEntries[currentDay];
        }
        else
        {
            //It doesn't exist, create it first 
            todaysChecklistEntries = new ObservableCollection<ChecklistEntry>();
            //Then add the checklist to the dictionary
            checklistEntries.Add(currentDay, todaysChecklistEntries);
        }
        //Finally, bind the checklist to xaml
        ToDoList.ItemsSource = todaysChecklistEntries;
    }

    private void GoBackADayButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(-1);
        //Update the date picker view in xaml
        datePicker.Date = currentDay;
        UpdateCurrentChecklist();
    }

    private void DatePickerPressed(object sender, EventArgs e)
    {
        //Cast the sender object to a DatePicker
        DatePicker datePicker = (DatePicker)sender;
        //Update the current day with the DatePicker's new day
        currentDay = datePicker.Date;
        UpdateCurrentChecklist();
    }

    private void GoForwardADayButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(1);
        //Update the date picker view in xaml
        datePicker.Date = currentDay;
        UpdateCurrentChecklist();
    }

    private async void AddToDoItemButtonPressed(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddChecklistItemPage(todaysChecklistEntries));
    }

    private async void DeleteChecklistButtonPressed(object sender, EventArgs e)
    {
        //Make sure somethingis actually selected
        if (ToDoList.SelectedItem != null)
        {
            //Get the currently selected entry
            ChecklistEntry tappedEntry = (ChecklistEntry)ToDoList.SelectedItem;
            bool answer = await DisplayAlert("Confirm delete:", $"Are you sure you want to remove '{tappedEntry.ToDoItemName}'?", "Yes", "No");
            if (answer)
            {
                todaysChecklistEntries.Remove(tappedEntry);
            }
        }
        else
        {
            await DisplayAlert("Notice", $"No item selected to delete", "Ok");
        }
    }

    private async void CheckoffAllChecklistItemsButtonPressed(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Check all", $"Are you sure you want to check off all ({todaysChecklistEntries.Count}) of your todo items?", "Yes", "No");
        if (answer)
        {
            foreach (ChecklistEntry todoItem in todaysChecklistEntries)
            {
                todoItem.IsCompleted = true;
            }
        }
    }
}