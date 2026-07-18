using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Ingredients.Queries.GetIngredients;

public sealed class GetIngredientsQueryHandler
{
    private readonly IIngredientRepository _repository;

    public GetIngredientsQueryHandler(
        IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<IngredientResponse>> Handle(
        GetIngredientsQuery query,
        CancellationToken cancellationToken = default)
    {
        var ingredients = await _repository.GetAllAsync(
            cancellationToken);

        return ingredients
            .Select(IngredientResponse.FromDomain)
            .ToList();
    }
}