using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Ingredients.Queries.GetIngredientById;

public sealed class GetIngredientByIdQueryHandler
{
    private readonly IIngredientRepository _repository;

    public GetIngredientByIdQueryHandler(
        IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IngredientResponse?> Handle(
        GetIngredientByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var ingredient = await _repository.GetByIdAsync(
            query.IngredientId,
            cancellationToken);

        return ingredient is not null
            ? IngredientResponse.FromDomain(ingredient)
            : null;
    }
}