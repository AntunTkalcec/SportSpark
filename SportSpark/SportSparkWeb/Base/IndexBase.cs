using Microsoft.AspNetCore.Components;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkWeb.Services;
using Microsoft.JSInterop;
using SportSparkWeb.Models;

namespace SportSparkWeb.Base;

public class IndexBase : ComponentBase, IAsyncDisposable
{
    [Inject]
    public IRestService RestService { get; set; }
    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;
    public UserDTO? User { get; set; }
    public List<EventDTO> Events { get; set; }
    public string ErrorMsg { get; set; }

    public Lazy<Task<IJSObjectReference>> moduleTask = default!;
    public LatLongWrapperDTO LatLongWrapper { get; set;}
    [Inject]
    public NavigationManager NavManager { get; set; }

    public IndexBase()
    {
        moduleTask = new(() => JSRuntime!.InvokeAsync<IJSObjectReference>(
            identifier: "import",
            args: "/js/geolocationJsInterop.js")
        .AsTask());
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                await GetLocationAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task GetLocationAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("getCurrentPosition", DotNetObjectReference.Create(this), nameof(OnSuccessAsync));
    }

    [JSInvokable]
    public async Task OnSuccessAsync(GeoCoordinates geoCoordinates)
    {
        LatLongWrapper = new()
        {
            Latitude = geoCoordinates.Latitude,
            Longitude = geoCoordinates.Longitude
        };
        Events = await RestService.GetEventsNearUserAsync(6000, LatLongWrapper);
        StateHasChanged();
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    public void GoToEventDetails(int id)
    {
        NavManager.NavigateTo($"eventDetails/{id}");
    }
}
