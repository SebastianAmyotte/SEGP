using CommunityToolkit.Mvvm.ComponentModel;

namespace SEGP7.Pages
{
    [System.Xml.Serialization.XmlInclude(typeof(String))]
    [System.Xml.Serialization.XmlInclude(typeof(bool))]

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
