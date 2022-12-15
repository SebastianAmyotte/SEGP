using Plugin.Maui.Audio;

namespace SEGP7.Pages;

// Author: Sebastian Amyotte
// Description: The destress countdown page

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
        // Create the audio manager
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
        // Begin drawing the circle
        ProgressView.Drawable = _arcProgress;
        if (soundRequest != "None")
        {
            PlayAsync(soundRequest);
        }
        // Set the background image
        backgroundImage.Source = backgroundRequest.ToLower().Replace(" ", "") + ".png";
        backgroundImage.WidthRequest = 1000;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Stop the player
        if (player != null)
        {
            player.Stop();
        }
    }
    
    // Begin playing
    async void PlayAsync(string fileName)
    {
        player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync($"Sounds/{fileName}.mp3"));
        player.Play();
    }

    // When the user taps anywhere, confirm if the user wants to stop the destress session
    async void CountdownTapped(object sender, EventArgs e)
    {
        // Confirmation popup
        bool response = await DisplayAlert("", "End session?", "Yes", "Cancel");
        if (response)
        {
            if (player != null)
            {
                player.Stop();
            }
            await Navigation.PopAsync();
        }
    }

    // Author: https://mallibone.com/post/dotnetmaui-countdown-button
    // Implemented by: Sebastian Amyotte
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
            // Update at the same rate of the screen refreshing
            await Task.Delay((int)Math.Round(1000 / DeviceDisplay.Current.MainDisplayInfo.RefreshRate));
        }
        if (player != null)
        {
            player.Stop();
        }
        await Navigation.PopAsync();
        // Reset the view
    }

    // Convert user input from picker to duration
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

// Author: https://mallibone.com/post/dotnetmaui-countdown-button
// Implemented by: Sebastian Amyotte
public class ProgressArc : IDrawable
{
    public double Progress { get; set; }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeSize = 10;
        canvas.StrokeColor = Colors.Black;
        canvas.DrawArc(5, 5, (dirtyRect.Width - 10), (dirtyRect.Height - 10), 0, 360, false, false);
        // Draw a circle in the center of the scree
        var endAngle = 90 - (int)Math.Round(Progress * 360, MidpointRounding.AwayFromZero);
        canvas.StrokeColor = Color.FromRgba("6599ff");
        canvas.DrawArc(5, 5, (dirtyRect.Width - 10), (dirtyRect.Height - 10), 90, endAngle, false, false);
        canvas.StrokeColor = Color.FromRgba(0, 0, 0, 50);
        canvas.FillColor = Color.FromRgba(0, 0, 0, 80);
        canvas.FillCircle(125, 125, 50);
    }
}