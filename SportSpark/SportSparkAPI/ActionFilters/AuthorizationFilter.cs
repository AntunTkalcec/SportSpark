using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SportSparkAPI.ActionFilters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var httpUser = context.HttpContext.User;

            if (!httpUser.Identity.IsAuthenticated) context.Result = new ForbidResult();
            else base.OnActionExecuting(context);
        }
    }
}
