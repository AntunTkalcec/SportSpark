using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using SportSpark.Helpers;
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
            builder.Services.AddSingleton<IRestService, RestService>();
            builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();

            //views
            builder.Services.AddSingleton<RegisterView>();
            builder.Services.AddSingleton<SignInView>();
            builder.Services.AddSingleton<HomeView>();

            //viewmodels
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<SignInViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();

            return builder.Build();
        }
    }
}