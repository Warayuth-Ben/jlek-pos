namespace JLek.POS.Api.Requests;

public sealed record UpdateProductDetailsRequest(
    string Name,
    string? Description,
    int? DisplayOrder);