using Plugin.LocalNotification;

namespace SEGP.Pages;

public partial class AddNotification : ContentPage
{
    public AddNotification()
    {
        InitializeComponent();
    }

    async void OnScheduleNotificationPressed(object sender, EventArgs e)
    {
        System.TimeSpan interval = CheckUserInterval();
        var request = new NotificationRequest
        {
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now,
                NotifyRepeatInterval = interval,
            }
        };
        await Navigation.PopAsync();
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

    async void OnCancelPressed(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}