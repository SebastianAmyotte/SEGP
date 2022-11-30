using Plugin.Maui.Audio;

namespace SEGP.Pages;
public partial class DestressVideoPage : ContentPage
{
    int durationInMilliseconds;
    String soundRequest;
    String backgroundRequest;
    ProgressArc _arcProgress = new ProgressArc();
    IAudioManager audioManager;
    IAudioPlayer player;

    public DestressVideoPage(String duration, String soundRequest, String backgroundRequest, IAudioManager audioManager)
	{
        this.audioManager = audioManager;
        this.durationInMilliseconds = StringToMillis(duration);
        this.soundRequest = soundRequest;   
        this.backgroundRequest = backgroundRequest;
        InitializeComponent();
        UpdateArc();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ProgressView.Drawable = _arcProgress;
        PlayAsync(soundRequest);
        backgroundImage.Source = "Raw/Backgrounds/" + backgroundRequest + ".gif";
        backgroundImage.WidthRequest = 1000;
    }

    async void PlayAsync(string fileName)
    {
        player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync($"Sounds/{fileName}.mp3"));
        player.Play();
    }


    async void CountdownTapped(object sender, EventArgs e)
    {
        //Popup
        bool response = await DisplayAlert("", "End session?", "Yes", "Cancel");
        if (response)
        {
            player.Stop();
            await Navigation.PopAsync();
        } else
        {
            //Do nothing
        }
        
    }

    //Author: https://mallibone.com/post/dotnetmaui-countdown-button
    private async void UpdateArc()
    {
        DateTime startTime = DateTime.Now;
        var elapsedTime = (DateTime.Now - startTime);
        int secondsRemaining;
        double progressPercent;
        while (elapsedTime.TotalMilliseconds < durationInMilliseconds)
        {
            elapsedTime = (DateTime.Now - startTime);
            secondsRemaining = (int)(durationInMilliseconds - elapsedTime.TotalMilliseconds) / 1000;
            ProgressButton.Text = $"{secondsRemaining}";

            progressPercent = elapsedTime.TotalMilliseconds;
            progressPercent %= durationInMilliseconds;

            _arcProgress.Progress = progressPercent / (float)durationInMilliseconds;
            ProgressView.Invalidate();
            //Update at the same rate of the screen refreshing
            await Task.Delay((int)Math.Round(1000 / DeviceDisplay.Current.MainDisplayInfo.RefreshRate));
        }
        player.Stop();
        await Navigation.PopAsync();
        // Reset the view
    }

    int StringToMillis(String userInput)
    {
        switch(userInput)
        {
            case "15 seconds":
                return 15 * 1000;
            case "30 seconds":
                return 30 * 1000;
            case "1 minute":
                return 60 * 1000;
            case "2 minutes":
                return 120 * 1000;
            case "3 minutes":
                return 180 * 1000;
            case "4 minutes":
                return 240 * 1000;
            case "5 minutes":
                return 300 * 1000;
            default:
                return 5 * 1000;
        }
    }

}

//Author: https://mallibone.com/post/dotnetmaui-countdown-button
public class ProgressArc : IDrawable
{
    public double Progress { get; set; }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeSize = 10;
        canvas.StrokeColor = Colors.Black;
        canvas.DrawArc(5, 5, (dirtyRect.Width - 10), (dirtyRect.Height - 10), 0, 360, false, false);
        var endAngle = 90 - (int)Math.Round(Progress * 360, MidpointRounding.AwayFromZero);
        canvas.StrokeColor = Color.FromRgba("6599ff");
        canvas.DrawArc(5, 5, (dirtyRect.Width - 10), (dirtyRect.Height - 10), 90, endAngle, false, false);
    }
}