using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Ingredients.Commands.SetIngredientAvailability;

public sealed class SetIngredientAvailabilityCommandHandler
{
    private readonly IIngredientRepository _repository;

    public SetIngredientAvailabilityCommandHandler(
        IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IngredientResponse> Handle(
        SetIngredientAvailabilityCommand command,
        CancellationToken cancellationToken = default)
    {
        var ingredient = await _repository.GetByIdAsync(
            command.IngredientId,
            cancellationToken);

        if (ingredient is null)
        {
            throw new InvalidOperationException("Ingredient not found.");
        }

        ingredient.SetAvailability(command.Status);

        await _repository.UpdateAsync(
            ingredient,
            cancellationToken);

        return IngredientResponse.FromDomain(ingredient);
    }
}