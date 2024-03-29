﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using SportSpark.Services;
using SportSpark.ViewModels;
using SportSpark.Views;
using SportSpark.Views.ContentViews;

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
                .UseMauiMaps()
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
            builder.Services.AddSingleton<IRestService, RestService>();
            builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();

            //views
            builder.Services.AddSingleton<RegisterView>();
            builder.Services.AddSingleton<SignInView>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddSingleton<MenuView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<CreateEventView>();
            builder.Services.AddTransient<FriendsView>();
            builder.Services.AddTransient<FriendsListView>();
            builder.Services.AddTransient<SeeMoreView>();
            builder.Services.AddTransient<EventVisibleToModal>();
            builder.Services.AddTransient<EventDetailsView>();

            //viewmodels
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<SignInViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<CreateEventViewModel>();
            builder.Services.AddTransient<FriendsViewModel>();
            builder.Services.AddTransient<FriendsListViewModel>();
            builder.Services.AddTransient<SeeMoreViewModel>();
            builder.Services.AddTransient<EventVisibleToViewModel>();
            builder.Services.AddTransient<EventDetailsViewModel>();

            return builder.Build();
        }
    }
}