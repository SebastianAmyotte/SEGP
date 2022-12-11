namespace SEGP7.Pages;

public partial class AddNotificationPage : ContentPage
{
	public AddNotificationPage()
	{
		InitializeComponent();
	}

	private void OnScheduleNotificationButtonPressed(object sender, EventArgs e)
	{
		
	}

    private void OnCancelButtonPressed(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}