using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkWeb.Services;

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
}
