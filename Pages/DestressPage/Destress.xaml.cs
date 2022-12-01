//Author: Sebastian Amyotte
//Reviewer: Sebastian Amyotte
using Plugin.Maui.Audio;
namespace SEGP.Pages;

public partial class Destress : ContentPage
{
    String duration = "";
    String sound = "";
    String background = "";
    IAudioManager audioManager;

    String[] listOfSounds = new String[] {
        //Sounds go here (No extension!)
        "Ocean Waves",
    };

    String[] listOfBackgrounds = new String[] {
        //Sounds go here (No extension!)
        "Ocean Waves",
        "Ocean Waves 2"
    };

    public Destress(IAudioManager audioManager)
	{
        InitializeComponent();
        VerifyEntries();
        soundPicker.ItemsSource = listOfSounds;
        backgroundPicker.ItemsSource = listOfBackgrounds;
        this.audioManager = audioManager;
    }

	async void StartDestressingButton(object sender, EventArgs e)
	{
        //Verify selections
        //Push page
		await AsyncPushDestressPage();
    }

    async void StartDestressingButtonDebug(object sender, EventArgs e)
    {
        //Push page
        await Navigation.PushAsync(new DestressVideoPage("30 seconds", "Ocean Waves", "ocean_waves", audioManager));
    }

    async Task AsyncPushDestressPage() {
        SubmitButton.IsEnabled = false;
        await Navigation.PushAsync(new DestressVideoPage(duration, sound, background, audioManager));
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