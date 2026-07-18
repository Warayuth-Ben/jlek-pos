using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.AddOptionGroup;

public sealed record AddOptionGroupCommand(
    ProductId ProductId,
    string Name,
    SelectionRule Rule);