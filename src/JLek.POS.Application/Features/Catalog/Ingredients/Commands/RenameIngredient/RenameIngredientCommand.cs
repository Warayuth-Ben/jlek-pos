using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Ingredients.Commands.RenameIngredient;

public sealed record RenameIngredientCommand(
    IngredientId IngredientId,
    string Name);