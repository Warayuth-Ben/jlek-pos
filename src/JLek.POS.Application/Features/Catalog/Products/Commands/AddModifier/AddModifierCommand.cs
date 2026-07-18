using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.AddModifier;

public sealed record AddModifierCommand(
    ProductId ProductId,
    string Name,
    decimal? PriceAdjustment);