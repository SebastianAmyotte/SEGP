using CommunityToolkit.Mvvm.ComponentModel;

namespace SEGP7.Pages
{
	public class NotificationsEntry : ObservableObject
	{
		public String notificationName;
		public DateTime timeSet;

		public String NotificationTitle { get { return notificationName + ' ' + timeSet; } }
		
		// Required for XML Serialization
		public NotificationsEntry() { }
        public NotificationsEntry(String entry, DateTime timeSetFor)
		{
			this.notificationName = entry;
			this.timeSet = timeSetFor;
		}
	}
}

