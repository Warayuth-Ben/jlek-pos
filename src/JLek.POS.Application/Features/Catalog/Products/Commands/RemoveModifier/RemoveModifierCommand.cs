using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.RemoveModifier;

public sealed record RemoveModifierCommand(
    ProductId ProductId,
    ModifierId ModifierId);