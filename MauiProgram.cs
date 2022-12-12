using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using SEGP7.Pages;
using SEGP7.Firebase;
using CommunityToolkit.Maui;

namespace SEGP7;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddTransient<DestressPage>();
        builder.Services.AddSingleton<FirebaseAuthenticationController>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
