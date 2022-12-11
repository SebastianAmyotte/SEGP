using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Core.View;
using Microsoft.Maui.Controls;
using static Android.Views.ViewGroup;

namespace SEGP7;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Platform.Init(this, savedInstanceState);
        //Hide the controls and time
        Window.AddFlags(WindowManagerFlags.Fullscreen);
        //Hide the navigation bar
        //Obsolete override, as this was what I was told to do from Android
        //Source: https://developer.android.com/develop/ui/views/layout/edge-to-edge#kotlin
#pragma warning disable CS0618 // Type or member is obsolete
        ViewCompat.GetWindowInsetsController(Window.DecorView).Hide(WindowInsetsCompat.Type.NavigationBars());
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
        ViewCompat.GetWindowInsetsController(Window.DecorView).Hide(WindowInsetsCompat.Type.SystemBars());
#pragma warning restore CS0618 // Type or member is obsolete
    }
}