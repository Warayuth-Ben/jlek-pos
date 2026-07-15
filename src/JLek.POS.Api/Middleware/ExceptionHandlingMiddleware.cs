using System.Net;
using System.Text.Json;
using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessRuleValidationException ex)
        {
            _logger.LogWarning(
                "Business rule violation: {Message}", ex.Message);

            await WriteProblemDetailsAsync(
                context,
                StatusCodes.Status400BadRequest,
                "Business rule validation failed",
                ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(
                "Invalid operation: {Message}", ex.Message);

            await WriteProblemDetailsAsync(
                context,
                StatusCodes.Status404NotFound,
                "Resource not found",
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, "An unhandled exception occurred.");

            await WriteProblemDetailsAsync(
                context,
                StatusCodes.Status500InternalServerError,
                "An internal error occurred.",
                "An unexpected error occurred. Please try again later.");
        }
    }

    private static async Task WriteProblemDetailsAsync(
        HttpContext context,
        int statusCode,
        string title,
        string detail)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;

        var problemDetails = new
        {
            type = $"https://httpstatuses.io/{statusCode}",
            title,
            status = statusCode,
            detail,
            instance = context.Request.Path
        };

        var json = JsonSerializer.Serialize(problemDetails);

        await context.Response.WriteAsync(json);
    }
}