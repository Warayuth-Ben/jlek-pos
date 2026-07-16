using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.SetVisibility;

public sealed record SetVisibilityCommand(
    ProductId ProductId,
    ProductVisibility Visibility);