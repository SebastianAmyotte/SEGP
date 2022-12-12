using CommunityToolkit.Mvvm.ComponentModel;

namespace SEGP7.Pages
{
	public class NotificationsEntry : ObservableObject
	{
		String notificationName;
		DateTime timeSet;


		public String NotificationName { get { return notificationName + " " + timeSet; } set { SetProperty(ref notificationName, value); } }

		public NotificationsEntry(String entry, DateTime timeSetFor)
		{
			this.notificationName = entry;
			this.timeSet = timeSetFor;
		}
	}
}

