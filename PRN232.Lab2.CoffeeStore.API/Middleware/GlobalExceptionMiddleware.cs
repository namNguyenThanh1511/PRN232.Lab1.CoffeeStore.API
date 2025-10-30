using Microsoft.AspNetCore.Diagnostics;
using PRN232.Lab2.CoffeeStore.Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace PRN232.Lab2.CoffeeStore.API.Middleware
{
    public class GlobalExceptionMiddleware : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "❌ An error occurred: {Message}", exception.Message);

            object response;
            int statusCode;

            if (exception is BaseDomainException domainEx)
            {
                statusCode = (int)domainEx.StatusCode;
                response = statusCode switch
                {
                    400 => ResponseBuilder.BadRequest(domainEx.Message),
                    401 => ResponseBuilder.Unauthorized(domainEx.Message),
                    403 => ResponseBuilder.Forbidden(domainEx.Message),
                    404 => ResponseBuilder.NotFound(domainEx.Message),
                    _ => ResponseBuilder.Error(domainEx.Message)
                };
            }
            else
            {
                // Lỗi không mong đợi
                statusCode = (int)HttpStatusCode.InternalServerError;
                response = ResponseBuilder.InternalServerError("Đã xảy ra lỗi không mong đợi.");
            }

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await httpContext.Response.WriteAsync(json, cancellationToken);

            return true;
        }
    }
}
