namespace JLek.POS.Api.Requests;

public sealed record CreateProductRequest(
    string Name,
    Guid CategoryId);