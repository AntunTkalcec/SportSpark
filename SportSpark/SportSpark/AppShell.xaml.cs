using SportSpark.Views;

namespace SportSpark
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(StartingView), typeof(StartingView));
            Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
            Routing.RegisterRoute(nameof(SignInView), typeof(SignInView));
        }
    }
}