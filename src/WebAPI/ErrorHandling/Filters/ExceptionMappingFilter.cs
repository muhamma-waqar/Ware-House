using Application.Common.Exceptions;
using Infrastructure.Authentication.External.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.ErrorHandling.Filters
{
    public class ExceptionMappingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = GetExceptionResult(context.Exception);
            context.ExceptionHandled = true;
        }

        private static IActionResult GetExceptionResult(Exception exception)
            => exception switch
            {
                InputValidationException e => HandleValidationException(e),
                EntityNotFoundException e => HandleNotFoundException(e),
                ExternalAuthenticationPreventedException e => HandleCannotAuthenticationExternal(e),
                _ => HandleUnknownException(exception)
            };

        private static IActionResult HandleCannotAuthenticationExternal(ExternalAuthenticationPreventedException e)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status503ServiceUnavailable,
                Title = "External authentication was prevented. Authentication provider might be unavailable. Try again later.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.4"
            };

            return new ObjectResult(details) { StatusCode = StatusCodes.Status503ServiceUnavailable };
        }

        private static IActionResult HandleUnknownException(Exception _)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occured while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            return new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };
        }

        private static IActionResult HandleValidationException(InputValidationException exception)
        {
            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            return new BadRequestObjectResult(details);
        }

        private static IActionResult HandleNotFoundException(EntityNotFoundException exception)
        {
            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            return new NotFoundObjectResult(details);
        }
    }
}
