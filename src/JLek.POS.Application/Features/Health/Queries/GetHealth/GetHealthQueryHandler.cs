namespace JLek.POS.Application.Features.Health.Queries.GetHealth;

public sealed class GetHealthQueryHandler
{
    public Task<HealthResponse> Handle(
        GetHealthQuery query,
        CancellationToken cancellationToken = default)
    {
        var response = new HealthResponse(
            Status: "Healthy",
            Timestamp: DateTime.UtcNow,
            Version: "1.0.0");

        return Task.FromResult(response);
    }
}