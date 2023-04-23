namespace SportSpark.Services
{
    public static class SettingsManager
    {
#if DEBUG
        public const string BaseURL = "https://127.0.0.1:7181/api/v1";
#else
        public static string BaseURL = "https://sportsparkapi.azurewebsites.net/api/v1";
#endif
    }
}
