using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Stock.API.ExceptionHandlers;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        const int internalServerErrorHttpStatusCode = (int)HttpStatusCode.InternalServerError;

        ProblemDetails problemDetails = new() 
        {
            Title = ReasonPhrases.GetReasonPhrase(internalServerErrorHttpStatusCode),
            Detail = "An error occurred while processing your request",
            Status = internalServerErrorHttpStatusCode,
            Instance = httpContext.Request.Path.Value
        };

        string serializedProblemDetails = JsonSerializer.Serialize(problemDetails);

        _logger.LogError("An error occurred: {ErrorMessage}", exception.Message);

        httpContext.Response.StatusCode = internalServerErrorHttpStatusCode;
        httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;

        await httpContext.Response.WriteAsync(serializedProblemDetails, cancellationToken: cancellationToken);

        return httpContext.Response.HasStarted;
    }
}