using SportSpark.Views;
using System.IdentityModel.Tokens.Jwt;

namespace SportSpark
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
            Routing.RegisterRoute(nameof(SignInView), typeof(SignInView));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(MenuView), typeof(MenuView));
            Routing.RegisterRoute(nameof(ProfileView), typeof(ProfileView));

            if (CheckIfAuthenticated())
            {
                shellContent.ContentTemplate = new DataTemplate(typeof(HomeView));
                shellContent.Route = nameof(HomeView);
            }
        }

        private static bool CheckIfAuthenticated()
        {
            string accessToken = Preferences.Get("access_token", "");
            if (string.IsNullOrEmpty(accessToken))
            {
                return false;
            }
            JwtSecurityTokenHandler jwtHandler = new();
            var jwtToken = jwtHandler.ReadJwtToken(accessToken);

            if (jwtToken.ValidTo <= DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
    }
}