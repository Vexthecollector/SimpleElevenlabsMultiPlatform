﻿using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using CommunityToolkit.Maui;


namespace SimpleElevenlabsAndroid;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton(AudioManager.Current);
		builder.UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}