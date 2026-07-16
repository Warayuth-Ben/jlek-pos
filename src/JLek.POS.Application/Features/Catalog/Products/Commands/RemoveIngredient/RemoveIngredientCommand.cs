using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.RemoveIngredient;

public sealed record RemoveIngredientCommand(
    ProductId ProductId,
    IngredientId IngredientId);