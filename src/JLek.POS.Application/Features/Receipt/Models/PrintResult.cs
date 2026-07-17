namespace JLek.POS.Application.Features.Receipt.Models;

public sealed record PrintResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public DateTime StartedAt { get; init; }
    public DateTime FinishedAt { get; init; }
    public TimeSpan Duration => FinishedAt - StartedAt;
    public string PrinterName { get; init; } = string.Empty;
    public int Copies { get; init; } = 1;
    public string Status { get; init; } = string.Empty;
}