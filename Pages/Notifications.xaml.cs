//Primary author: Calvin Larson
//Reviewer: Calvin Larson
using Plugin.LocalNotification;

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

    void OnDeleteButtonPressed(object sender, EventArgs e)
    {
        DisplayAlert("Deleted item", "Item deleted! (Not actually)", "OK");
    }
}