using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace ReservationWebAPI
{
    public class TokenAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private bool _shouldIgnoreExpirationTime;
        public TokenAuthorizationFilter(bool shouldIgnoreExpirationTime = false)
        {
            _shouldIgnoreExpirationTime = shouldIgnoreExpirationTime;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHandler = context.HttpContext.RequestServices.GetService<ITokenAuthorizationHandler>();
            var userInfoFromToken = context.HttpContext.RequestServices.GetService<IUserInfoFromToken>();
            try
            {
                var authorization = context.HttpContext.Request.Headers["Authorization"];
                var token = JsonConvert.DeserializeObject<Token>(authorization);
                var isTokenValid = _shouldIgnoreExpirationTime ? authorizationHandler.CheckIfTokenIsValid(token) : authorizationHandler.CheckIfTokenIsValidAndUnexpired(token);
                if (!isTokenValid)
                    ContextResultMaker.CreateUnauthorizedResult(context);
                userInfoFromToken.UserId = authorizationHandler.GetUserIdFromToken(token);
            }
            catch
            {
                ContextResultMaker.CreateUnauthorizedResult(context);
            }
        }
    }
}
