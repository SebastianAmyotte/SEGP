using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using SEGP7.Pages;
using SEGP7.Firebase;
using CommunityToolkit;

namespace SEGP7;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			//.UseMauiCommunityToolkit()
			//Needs plugin CommunityToolkit.Maui in order to function.
			//Was needed for switching focuses between entries when presing the "enter" button
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddTransient<DestressPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
