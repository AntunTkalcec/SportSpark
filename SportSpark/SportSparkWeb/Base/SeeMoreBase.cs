using Microsoft.AspNetCore.Components;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkWeb.Services;

namespace SportSparkWeb.Base;

public class SeeMoreBase : ComponentBase
{

    [Inject]
    public IRestService RestService { get; set; }

    public List<EventDTO> Events { get; set; }

    public string ErrorMsg { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                Events = await RestService.SeeMoreEventsAsync();
                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ErrorMsg = ex.Message;
        }
    }
}
