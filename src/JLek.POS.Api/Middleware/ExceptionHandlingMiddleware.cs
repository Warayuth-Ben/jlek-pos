using System.Text.Json;
using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessRuleValidationException ex)
        {
            _logger.LogWarning("Business rule violation: {Message}", ex.Message);

            await WriteProblemDetailsAsync(
                context,
                StatusCodes.Status409Conflict,
                "Business rule violation",
                ex.Message,
                ex);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Resource not found: {Message}", ex.Message);

            await WriteProblemDetailsAsync(
                context,
                StatusCodes.Status404NotFound,
                "Resource not found",
                ex.Message,
                ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Unauthorized access: {Message}", ex.Message);

            await WriteProblemDetailsAsync(
                context,
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                ex.Message,
                ex);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Invalid operation: {Message}", ex.Message);

            await WriteProblemDetailsAsync(
                context,
                StatusCodes.Status409Conflict,
                "Invalid operation",
                ex.Message,
                ex);
        }
        catch (NotImplementedException ex)
        {
            _logger.LogWarning("Not implemented: {Message}", ex.Message);

            await WriteProblemDetailsAsync(
                context,
                StatusCodes.Status501NotImplemented,
                "Not implemented",
                ex.Message,
                ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            await WriteProblemDetailsAsync(
                context,
                StatusCodes.Status500InternalServerError,
                "An internal error occurred",
                "An unexpected error occurred. Please try again later.",
                ex);
        }
    }

    private async Task WriteProblemDetailsAsync(
        HttpContext context,
        int statusCode,
        string title,
        string detail,
        Exception exception)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;

        var problemDetails = new
        {
            type = $"https://httpstatuses.io/{statusCode}",
            title,
            status = statusCode,
            detail,
            instance = context.Request.Path,
            traceId = context.TraceIdentifier,
            exception = _environment.IsDevelopment()
                ? exception.ToString()
                : null
        };

        var json = JsonSerializer.Serialize(problemDetails);

        await context.Response.WriteAsync(json);
    }
}