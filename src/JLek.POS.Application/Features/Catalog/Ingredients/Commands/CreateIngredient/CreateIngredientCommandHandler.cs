using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;
using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Ingredients.Commands.CreateIngredient;

public sealed class CreateIngredientCommandHandler
{
    private readonly IIngredientRepository _repository;

    public CreateIngredientCommandHandler(
        IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IngredientResponse> Handle(
        CreateIngredientCommand command,
        CancellationToken cancellationToken = default)
    {
        var ingredient = Ingredient.Create(command.Name);

        await _repository.AddAsync(
            ingredient,
            cancellationToken);

        return IngredientResponse.FromDomain(ingredient);
    }
}