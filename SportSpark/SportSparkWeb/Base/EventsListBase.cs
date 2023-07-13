using Microsoft.AspNetCore.Components;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkWeb.Base;

public class EventsListBase : ComponentBase
{
    [Parameter]
    public List<EventDTO> Events { get; set; }
    [Inject]
    public NavigationManager NavManager { get; set; }

    public void GoToEventDetails(int id)
    {
        NavManager.NavigateTo($"eventDetails/{id}");
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
}
