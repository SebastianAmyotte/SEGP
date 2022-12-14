using System.Collections.ObjectModel;
namespace SEGP7.Pages;

public partial class AddChecklistItemPage : ContentPage
{
    private ObservableCollection<ChecklistEntry> todaysChecklistEntries;

    public AddChecklistItemPage(ObservableCollection<ChecklistEntry> todaysChecklistEntries)
    {
        this.todaysChecklistEntries = todaysChecklistEntries;
        InitializeComponent();
	}

    async void OnConfirmAddChecklistItemButtonPressed(object sender, EventArgs e)
    {
        todaysChecklistEntries.Add(new ChecklistEntry(ToDoItemTextEntry.Text));
        await Navigation.PopAsync();
    }

    async void OnCancelButtonPressed(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}