using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Ingredients.Queries.GetIngredientById;

public sealed record GetIngredientByIdQuery(
    IngredientId IngredientId);