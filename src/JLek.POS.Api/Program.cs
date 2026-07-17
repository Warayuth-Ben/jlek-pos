using JLek.POS.Api.Middleware;
using JLek.POS.Infrastructure;
using JLek.POS.Api.Endpoints;
using JLek.POS.Application;
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
app.MapOrderEndpoints();
app.MapCatalogEndpoints();
app.MapTableEndpoints();
app.MapKitchenEndpoints();
app.MapPaymentEndpoints();
app.Run();

public partial class Program { }