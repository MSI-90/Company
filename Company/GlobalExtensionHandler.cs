using Contracts;
using Entities.ErorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Company;

public class GlobalExtensionHandler : IExceptionHandler
{
    private readonly ILoggerManager _logger;
    public GlobalExtensionHandler(ILoggerManager logger) => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            httpContext.Response.StatusCode = contextFeature.Error switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            _logger.LogError($"Что-то пошло не так: {exception.Message}");

            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = contextFeature.Error.Message,
            }.ToString());
        }

        return true;
    }
}
