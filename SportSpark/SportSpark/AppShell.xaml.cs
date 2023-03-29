using SportSpark.Views;

namespace SportSpark
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(FirstStartupView), typeof(FirstStartupView));
            Routing.RegisterRoute(nameof(StartingView), typeof(StartingView));
        }
    }
}