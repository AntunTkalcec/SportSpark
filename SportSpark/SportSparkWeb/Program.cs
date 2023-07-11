using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkInfrastructureLibrary.Services;
using SportSparkWeb;
using SportSparkWeb.Auth;
using SportSparkWeb.Models;
using SportSparkWeb.Services;
using SportSparkWeb.StateStores;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7181/api/v1/") });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRestService, RestService>();
builder.Services.AddScoped<SportSparkWeb.Services.UserService>();
builder.Services.AddSingleton<UserData>();
builder.Services.AddBlazoredToast();
builder.Services.AddSingleton<UserStateStore>();

await builder.Build().RunAsync();