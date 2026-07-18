using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.RemoveOptionGroup;

public sealed record RemoveOptionGroupCommand(
    ProductId ProductId,
    OptionGroupId OptionGroupId);