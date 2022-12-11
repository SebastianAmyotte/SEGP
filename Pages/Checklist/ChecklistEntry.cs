using CommunityToolkit.Mvvm.ComponentModel;

namespace SEGP7.Pages
{
    public class ChecklistEntry : ObservableObject
    {
        String toDoItemName;
        bool isCompleted = false;

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
