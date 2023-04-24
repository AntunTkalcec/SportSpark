namespace SportSpark.Services
{
    public class NavigationService : INavigationService
    {
        public Task NavigateToAsync(string route, IDictionary<string, object> parameters = null)
        {
            if (parameters != null)
            {
                return Shell.Current.GoToAsync(route, true, parameters);
            }
            return Shell.Current.GoToAsync(route, true);
        }
    }
}
