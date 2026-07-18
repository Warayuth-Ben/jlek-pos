namespace JLek.POS.Printing.Models;

public sealed record PrinterStatus
{
    public bool IsOnline { get; init; }
    public bool HasPaper { get; init; }
    public bool IsBusy { get; init; }
    public string? ErrorMessage { get; init; }
}