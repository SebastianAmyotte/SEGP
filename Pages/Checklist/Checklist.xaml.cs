//Author: Sebastian Amyotte
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SEGP.Pages;

public partial class Checklist : ContentPage
{
    DateTime currentDay = DateTime.Today;
    Dictionary<DateTime, ObservableCollection<ChecklistEntry>> checklistEntries;
    ObservableCollection<ChecklistEntry> todaysChecklistEntries;

    public Checklist()
	{
		InitializeComponent();
        datePicker.Date = currentDay;
        checklistEntries = new Dictionary<DateTime, ObservableCollection<ChecklistEntry>>();
        todaysChecklistEntries = new ObservableCollection<ChecklistEntry>();
        checklistEntries.Add(currentDay, todaysChecklistEntries);
        ToDoList.ItemsSource = todaysChecklistEntries;
    }

    void UpdateCurrentChecklist()
    {
        if (checklistEntries.ContainsKey(currentDay))
        {
            todaysChecklistEntries = checklistEntries[currentDay];
            ToDoList.ItemsSource = todaysChecklistEntries;
        }
        else
        {
            todaysChecklistEntries = new ObservableCollection<ChecklistEntry>();
            checklistEntries.Add(currentDay, todaysChecklistEntries);
            ToDoList.ItemsSource = checklistEntries;
        }
    }
    void OnBackButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(-1);
        datePicker.Date = currentDay;
        UpdateCurrentChecklist();
    }

    void OnDatePickerChanged(object sender, EventArgs e)
    {
        DatePicker datePicker = (DatePicker)sender;
        currentDay = datePicker.Date;
        UpdateCurrentChecklist();
    }

    void OnNextButtonPressed(object sender, EventArgs e)
    {
        currentDay = currentDay.AddDays(1);
        datePicker.Date = currentDay;
        UpdateCurrentChecklist();
    }

    async void OnDeleteButtonPress(object sender, EventArgs e)
    {
        if (ToDoList.SelectedItem != null)
        {
            ChecklistEntry tappedEntry = (ChecklistEntry)ToDoList.SelectedItem;
            bool answer = await DisplayAlert("Confirm delete:", $"Are you sure you want to remove '{tappedEntry.ToDoItemName}'?", "Yes", "No");
            if (answer)
            {
                todaysChecklistEntries.Remove(tappedEntry);
            }
            UpdateCurrentChecklist();
        } else
        {
            await DisplayAlert("Notice", $"No item selected to delete", "Ok");
        }
    }

    async void OnAddToDoItemButtonPressed(object sender, EventArgs e) 
    {
        await Navigation.PushAsync(new AddToDoItemPage(todaysChecklistEntries));
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

    async public void AllToDoItems(object sender, EventArgs e)
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

