using CommunityToolkit.Mvvm.ComponentModel;

//Author: Sebastian Amyotte
//Description: A representation of a single checklist item

namespace SEGP7.Pages
{ 
    public class ChecklistEntry : ObservableObject
    {
        public String toDoItemName;
        public bool isCompleted = false;

        public String ToDoItemName { get { return toDoItemName; } set { SetProperty(ref toDoItemName, value); } }
        public bool IsCompleted { get { return isCompleted; } set { SetProperty(ref isCompleted, value); } }

        public ChecklistEntry(String entry)
        {
            this.toDoItemName = entry;
        }

        public void ToggleCompleted()
        {
            isCompleted = !isCompleted;
        }
    }
}
