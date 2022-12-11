using AndroidX.Core.View;
using Plugin.Maui.Audio;

namespace SEGP7.Pages;

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
        backgroundImage.Source = backgroundRequest.ToLower().Replace(" ", "") + ".png";
        System.Console.WriteLine(backgroundImage.Source);
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
        }
        else
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
        return userInput switch
        {
            "15 seconds" => 15 * 1000,
            "30 seconds" => 30 * 1000,
            "1 minute" => 60 * 1000,
            "2 minutes" => 120 * 1000,
            "3 minutes" => 180 * 1000,
            "4 minutes" => 240 * 1000,
            "5 minutes" => 300 * 1000,
            _ => 5 * 1000,
        };
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