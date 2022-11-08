namespace SEGP.Pages;

public partial class Notifications : ContentPage
{
	public Notifications()
	{
		InitializeComponent();
	}

	async void OnAddButtonPressed(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddNotification());
	}

	void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		//Deletes notification
	}
}