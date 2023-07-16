using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkWeb.Services;
using System.Globalization;

namespace SportSparkWeb.Base;

public class EventDetailsBase : ComponentBase
{
    [Inject]
    public IRestService RestService { get; set; }
    [Inject]
    public IJSRuntime JSRuntime { get; set; }
    [Parameter]
    public int Id { get; set; }
    public EventDTO Event { get; set; }
    public string ErrorMsg { get; set; }
    public string UserProfileImageData { get; set; }
    public Lazy<Task<IJSObjectReference>> moduleTask = default!;
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public IToastService ToastService { get; set; }

    public EventDetailsBase()
    {
        moduleTask = new(() => JSRuntime!.InvokeAsync<IJSObjectReference>(
            identifier: "import",
            args: "/js/geolocationJsInterop.js")
        .AsTask());
    }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Event = await RestService.GetEventByIdAsync(Id);

            UserProfileImageData = GetUserProfileImageData();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ErrorMsg = ex.Message;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                Event = await RestService.GetEventByIdAsync(Id);

                UserProfileImageData = GetUserProfileImageData();

                //var module = await moduleTask.Value;
                //await module.InvokeVoidAsync("initializeMaps", Event.LatLong); google maps is not free
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ErrorMsg = ex.Message;
            }
        }
    }

    private string GetUserProfileImageData()
    {
        if (Event.User.ProfileImageData is not null && Event.User.ProfileImageData.Length > 0)
        {
            return $"data:image/png;base64,{Convert.ToBase64String(Event.User.ProfileImageData)}";
        }

        return @"https://w7.pngwing.com/pngs/844/95/png-transparent-anonymity-person-computer-icons-word-of-mouth-silhouette-business-internet-thumbnail.png";
    }

    public static string GetImg(EventDTO ev)
    {
        switch (ev.EventType.Name)
        {
            case "Football":
                return "football.jpg";
            case "Chess":
                return "chess.jpg";
            case "Baseball":
                return "baseball.jpg";
            default:
                return "other_sport.png";
        }
    }

    public static string GetTime(string time)
    {
        DateTime parsedTime = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);
        return parsedTime.ToString("h:mm tt", CultureInfo.InvariantCulture);
    }

    public void GoToUserProfile(int? userId)
    {
        NavigationManager.NavigateTo($"/profile/{userId}");
        Console.WriteLine("TU SAM");
    }

    public async Task SaveEvent(EventDTO ev)
    {
        ToastService.ShowWarning("This is just for show. No event saving is implemented.");
    }
}
