using JLek.POS.Api.Middleware;
using JLek.POS.Infrastructure;
using JLek.POS.Api.Endpoints;
using JLek.POS.Application;
using JLek.POS.Application.Features.Health.Queries.GetHealth;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints
app.MapGet("/", () => Results.Ok("JLek POS API"));

// Health
app.MapGet("/health", async (
    [FromServices] GetHealthQueryHandler handler) =>
{
    var query = new GetHealthQuery();
    var result = await handler.Handle(query);
    return Results.Ok(result);
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