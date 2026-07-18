namespace JLek.POS.Printing.Models;

public sealed record PrintPayload
{
    public required ReadOnlyMemory<byte> Data { get; init; }
    public required string MimeType { get; init; }
    public required string Format { get; init; }
    public string? Description { get; init; }
}