namespace SEGP.Pages;

public partial class AddNotification : ContentPage
{
	public AddNotification()
	{
		InitializeComponent();
	}

	async void OnScheduleNotificationPressed (object sender, EventArgs e)
	{
        await Navigation.PopAsync();
    }
    async void OnCancelPressed(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}