//Primary Author : Woo Sik Choi
//Reviewer: Sebastian Amyotte
using CommunityToolkit.Mvvm.ComponentModel;

public class ChecklistEntry : ObservableObject
{

    public DateTime dates;
    String toDoItemName;
    bool isCompleted = false;

    public String ToDoItemName { get { return toDoItemName; } set { SetProperty(ref toDoItemName, value); } }
    public bool IsCompleted { get { return isCompleted; } set { SetProperty(ref isCompleted, value); } }

    public ChecklistEntry(String entry)
    {
        this.toDoItemName = entry;
        //this.dates = dates; //What is this for?
    }


    public String GetEntry()
    {
        return toDoItemName;
    }


    public void SetEntry(String listEntry)
    {
        this.toDoItemName = listEntry;
    }

    public void ToggleCompleted()
    {
        isCompleted = !isCompleted;
    }
}