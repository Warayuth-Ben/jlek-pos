using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Ingredients.Commands.SetIngredientAvailability;

public sealed record SetIngredientAvailabilityCommand(
    IngredientId IngredientId,
    IngredientStatus Status);