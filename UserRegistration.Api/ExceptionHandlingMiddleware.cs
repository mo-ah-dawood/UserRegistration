using System.Reflection;
using Microsoft.Extensions.Localization;
using UserRegistration.Api.Response;
using UserRegistration.Application.Exceptions;

namespace UserRegistration.Api
{


    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IStringLocalizer _localizer;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IStringLocalizerFactory factory)
        {
            _next = next;
            _logger = logger;
            _localizer = factory.Create(typeof(SharedResources));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            string message;
            string errorCode;
            if (exception is AppException httpResponseException)
            {
                message = _localizer[httpResponseException.Message];
                errorCode = httpResponseException.ErrorCode;
                statusCode = 400;
            }
            else
            {
                _logger.LogError(exception, "An unexpected error occurred.");
                message = _localizer[exception.Message ?? "Unknown exception"];
                errorCode = exception.GetType().ToString();
                statusCode = 500;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(GenericResult.Error(message, errorCode));
        }
    }
}