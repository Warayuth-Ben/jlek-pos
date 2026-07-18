using JLek.POS.Domain.Catalog;
using JLek.POS.Domain.Common.ValueObjects;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.RemoveSuggestedPrice;

public sealed record RemoveSuggestedPriceCommand(
    ProductId ProductId,
    Money Price);