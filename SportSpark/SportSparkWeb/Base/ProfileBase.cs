using Microsoft.AspNetCore.Components;
using SportSparkCoreSharedLibrary.DTOs;
using SportSparkWeb.Services;

namespace SportSparkWeb.Base;

public class ProfileBase : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    [Inject]
    public IRestService RestService { get; set; }

    public UserDTO User { get; set; }

    public string ErrorMsg { get; set; }

    public string UserProfileImageData { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            User = await RestService.GetUserAsync();
            UserProfileImageData = GetProfileImageData();

        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
        }
    }

    private string GetProfileImageData()
    {
        if (User.ProfileImageData is not null && User.ProfileImageData.Length > 0)
        {
            return $"data:image/png;base64,{Convert.ToBase64String(User.ProfileImageData)}";
        }

        return @"https://w7.pngwing.com/pngs/844/95/png-transparent-anonymity-person-computer-icons-word-of-mouth-silhouette-business-internet-thumbnail.png";
    }
}
