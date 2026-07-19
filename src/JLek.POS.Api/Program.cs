using JLek.POS.Api.Middleware;
using JLek.POS.Infrastructure;
using JLek.POS.Api.Endpoints;
using JLek.POS.Application;
using Microsoft.Extensions.Diagnostics.HealthChecks;
var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("Content-Type");
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks();

var app = builder.Build();

// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// Endpoints
app.MapGet("/", () => Results.Ok("JLek POS API"));

// Health
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            database = report.Entries.ContainsKey("ApplicationDbContext")
                ? report.Entries["ApplicationDbContext"].Status.ToString()
                : "Unhealthy",
            timestamp = DateTime.UtcNow
        });
        await context.Response.WriteAsync(result);
    }
});
app.MapOrderEndpoints();
app.MapCatalogEndpoints();
app.MapTableEndpoints();
app.MapKitchenEndpoints();
app.MapPaymentEndpoints();
app.MapReportingEndpoints();
app.MapReceiptEndpoints();
app.Run();

public partial class Program { }