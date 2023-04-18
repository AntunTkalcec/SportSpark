namespace SportSpark.Helpers
{
    public static class NavigationHelper
    {
        public static void ClearNavigationStack(INavigation navigation)
        {
            for (int i = 0; i < navigation.NavigationStack.Count; i++)
            {
                navigation.RemovePage(navigation.NavigationStack[i]);
            }
        }
    }
}
