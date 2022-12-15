using System.Collections.ObjectModel;
using Plugin.LocalNotification;

namespace SEGP7.Pages;

public partial class AddNotificationPage : ContentPage
{
	private ObservableCollection<NotificationsEntry> notificationEntries;

	public AddNotificationPage(ObservableCollection<NotificationsEntry> notificationEntries)
	{
		this.notificationEntries = notificationEntries;
		InitializeComponent();
		datePicker.Date = DateTime.Today;
		timePicker.Time = DateTime.Now.TimeOfDay;
	}

	async void OnScheduleNotificationButtonPressed(object sender, EventArgs e)
	{
		// method to set how often the notification goes off
		System.TimeSpan interval;
		try
		{
			interval = CheckUserInterval();
		}
		// if the method returns an error
		catch
		{
			// sets a default interval of 1 hour
			interval = System.TimeSpan.FromHours(1);//FIXME
		}

		DateTime futureDate = datePicker.Date + timePicker.Time; ;
		if (UserDateIsNotValid(futureDate))
		{
			await DisplayAlert("Time and date not set in future", "Please select a time in the future", "OK");
		}
		else
		{
			var request = new NotificationRequest
			{
				Title = title.Text,
				Schedule = new NotificationRequestSchedule
				{
					NotifyTime = futureDate.ToLocalTime(),
					NotifyRepeatInterval = interval,
				}
			};

			await LocalNotificationCenter.Current.Show(request);
			notificationEntries.Add(new NotificationsEntry(title.Text, futureDate));
			await Navigation.PopAsync();
		}
	}

	async void OnCancelButtonPressed(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

	private bool UserDateIsNotValid(DateTime userDate)
{
		return userDate < DateTime.Now ;
	}

	private System.TimeSpan CheckUserInterval()
	{
		switch (interval.SelectedIndex)
		{
			case 0:
				return TimeSpan.FromHours(1);
			case 1:
				return TimeSpan.FromDays(1);
			case 2:
				return TimeSpan.FromDays(7);
			case 3:
				return TimeSpan.FromDays(30);
			case 4:
				return TimeSpan.FromDays(365);
			default:
				throw new Exception("interval not selected");
		}
	}
}