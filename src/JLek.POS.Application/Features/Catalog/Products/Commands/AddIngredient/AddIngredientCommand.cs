using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.AddIngredient;

public sealed record AddIngredientCommand(
    ProductId ProductId,
    IngredientId IngredientId);