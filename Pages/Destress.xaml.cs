namespace SEGP.Pages;

public partial class Destress : ContentPage
{
	String duration = "";
    String sound = "";
    String background = "";

    public Destress()
	{
        InitializeComponent();
        VerifyEntries();

    }

	async void StartDestressingButton(object sender, EventArgs e)
	{
        //Verify selections
        
        //Push page
		await AsyncPushDestressPage();
    }

	async Task AsyncPushDestressPage() {
        SubmitButton.IsEnabled = false;
        await Navigation.PushAsync(new DestressVideoPage(duration, sound, background));
        SubmitButton.IsEnabled = true;
    }

	void DurationPicked(object sender, EventArgs e)
	{
        duration = GetItemFromPicker((Picker)sender);
        VerifyEntries();
    }
    void SoundPicked(object sender, EventArgs e)
    {
        sound = GetItemFromPicker((Picker)sender);
        VerifyEntries();
    }
    void BackgroundPicked(object sender, EventArgs e)
    {
        background = GetItemFromPicker((Picker)sender);
        VerifyEntries();
    }

    void VerifyEntries()
    {
        if (!(duration.Equals("") || sound.Equals("") || background.Equals("")))
        {
            SubmitButton.IsEnabled = true;
        } else
        {
            SubmitButton.IsEnabled = false;
        }
    }

    String GetItemFromPicker(Picker picker)
    {
        if (picker.SelectedIndex != -1)
        {
            return (String)picker.SelectedItem;
        } else
        {
            return "";
        }
    }
}