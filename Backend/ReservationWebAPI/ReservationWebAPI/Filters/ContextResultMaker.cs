using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReservationWebAPI
{
    public static class ContextResultMaker
    {
        public static void CreateUnauthorizedResult(AuthorizationFilterContext context)
        {
            context.Result = new ContentResult { StatusCode = StatusCodes.Status401Unauthorized, Content = "Authorization required" };
        }
    }
}
