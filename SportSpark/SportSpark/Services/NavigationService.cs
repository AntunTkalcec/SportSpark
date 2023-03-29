﻿namespace SportSpark.Services
{
    public class NavigationService : INavigationService
    {
        public Task NavigateToAsync(string route, IDictionary<string, object> parameters = null)
        {
            if (parameters != null)
            {
                Shell.Current.GoToAsync(route, parameters);
            }
            return Shell.Current.GoToAsync(route);
        }
    }
}