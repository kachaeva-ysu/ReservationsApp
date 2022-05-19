using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReservationWebAPI
{
    public class ExceptionHandlingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = StatusCodes.Status400BadRequest;
            var message = context.Exception.Message;
            
            if (context.Exception is NotFoundException)
                statusCode = StatusCodes.Status404NotFound;
            else if (context.Exception is ForbiddenException)
                statusCode = StatusCodes.Status403Forbidden;
            else if (!(context.Exception is BadRequestException))
                message = "Something went wrong. Please contact administrator";

            context.Result = GetContextResult(statusCode, message);
            context.ExceptionHandled = true;
        }

        private ContentResult GetContextResult(int statusCode, string message)
        {
            return new ContentResult
            {
                StatusCode = statusCode,
                ContentType = "application/json",
                Content = message
            };
        }
    }
}
