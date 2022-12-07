//Primary author: Sebastian Amyotte
using Plugin.LocalNotification;
using System.Collections.ObjectModel;

namespace SEGP.Pages;

public partial class AddToDoItemPage : ContentPage
{
    ObservableCollection<ChecklistEntry> todaysChecklistEntries;

    public AddToDoItemPage(ObservableCollection<ChecklistEntry> todaysChecklistEntries)
	{
        this.todaysChecklistEntries = todaysChecklistEntries;
		InitializeComponent();
	}

	async void OnAddToDoItemPressed(object sender, EventArgs e)
	{
        todaysChecklistEntries.Add(new ChecklistEntry(ToDoItemTextEntry.Text));
        await Navigation.PopAsync();
    }

    async void OnCancelPressed(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}