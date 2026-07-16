using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.UpdateProductDetails;

public sealed record UpdateProductDetailsCommand(
    ProductId ProductId,
    string Name,
    string? Description,
    int? DisplayOrder);