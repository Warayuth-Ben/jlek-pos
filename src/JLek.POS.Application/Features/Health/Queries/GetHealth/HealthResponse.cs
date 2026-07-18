namespace JLek.POS.Application.Features.Health.Queries.GetHealth;

public sealed record HealthResponse(
    string Status,
    DateTime Timestamp,
    string Version);