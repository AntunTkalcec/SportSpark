﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;
using SkiaSharp.Views.Maui.Controls.Hosting;
using SportSpark.Services;
using SportSpark.ViewModels;
using SportSpark.ViewModels.Base;
using SportSpark.Views;

namespace SportSpark
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-brands-400.ttf", "faBrands");
                    fonts.AddFont("fa-regular-400.ttf", "faRegular");
                    fonts.AddFont("fa-solid-900.ttf", "faSolid");
                });              

#if DEBUG
		builder.Logging.AddDebug();
#endif

            //services
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            //views
            builder.Services.AddSingleton<FirstStartupView>();
            builder.Services.AddSingleton<StartingView>();
            builder.Services.AddSingleton<RegisterView>();
            builder.Services.AddSingleton<SignInView>();

            //viewmodels
            builder.Services.AddTransient<BaseViewModel>();
            builder.Services.AddSingleton<FirstStartupViewModel>();
            builder.Services.AddSingleton<StartingViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();

            return builder.Build();
        }
    }
}