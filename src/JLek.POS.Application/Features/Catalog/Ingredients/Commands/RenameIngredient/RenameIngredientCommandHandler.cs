using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Ingredients.Commands.RenameIngredient;

public sealed class RenameIngredientCommandHandler
{
    private readonly IIngredientRepository _repository;

    public RenameIngredientCommandHandler(
        IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IngredientResponse> Handle(
        RenameIngredientCommand command,
        CancellationToken cancellationToken = default)
    {
        var ingredient = await _repository.GetByIdAsync(
            command.IngredientId,
            cancellationToken);

        if (ingredient is null)
        {
            throw new InvalidOperationException("Ingredient not found.");
        }

        ingredient.Rename(command.Name);

        await _repository.UpdateAsync(
            ingredient,
            cancellationToken);

        return IngredientResponse.FromDomain(ingredient);
    }
}