using Microsoft.AspNetCore.Mvc;

namespace SportSparkAPI.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        public int UserId
        {
            get
            {
                var userId = -1;
                _ = int.TryParse(this.User.Claims.FirstOrDefault(t => t.Type == "UserId")?.Value, out userId);
                return userId;
            }
        }
    }
}
