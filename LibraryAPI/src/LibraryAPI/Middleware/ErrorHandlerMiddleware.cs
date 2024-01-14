using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace LibraryAPI.BL
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                var resultMessage = JsonSerializer.Serialize(new { message = error.Message });
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException:
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        _logger.LogError(error, error.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        resultMessage = JsonSerializer.Serialize(new { message = "InternalServerError" });
                        break;
                }

                await response.WriteAsync(resultMessage);
            }
        }
    }
}