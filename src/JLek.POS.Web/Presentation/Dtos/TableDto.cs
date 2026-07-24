namespace JLek.POS.Web.Presentation.Dtos;

public sealed record TableDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
}