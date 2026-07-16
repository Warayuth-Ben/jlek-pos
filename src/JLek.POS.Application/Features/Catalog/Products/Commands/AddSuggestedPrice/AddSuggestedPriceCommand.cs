using JLek.POS.Domain.Catalog;
using JLek.POS.Domain.Common.ValueObjects;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.AddSuggestedPrice;

public sealed record AddSuggestedPriceCommand(
    ProductId ProductId,
    Money Price);