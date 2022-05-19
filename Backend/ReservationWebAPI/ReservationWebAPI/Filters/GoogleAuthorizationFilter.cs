using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ReservationWebAPI
{
    public class GoogleAuthorizationFilter: Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationHandler = context.HttpContext.RequestServices.GetService<IGoogleAuthorizationHandler>();
            var userInfoFromToken = context.HttpContext.RequestServices.GetService<IUserInfoFromToken>();
            var googleToken = context.HttpContext.Request.Headers["GoogleAuthorization"];
            if (!await authorizationHandler.CheckIfGoogleTokenIsValid(googleToken))
                ContextResultMaker.CreateUnauthorizedResult(context);
            userInfoFromToken.Email = await authorizationHandler.GetEmailFromGoogleToken(googleToken);
            userInfoFromToken.Name = await authorizationHandler.GetUserNameFromGoogleToken(googleToken);
        }
    }
}
