namespace JLek.POS.Web.Presentation.Models;

public sealed record TableViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public bool IsAvailable { get; init; }
    public bool IsSelected { get; set; }
    public int SeatCount { get; init; }
}
