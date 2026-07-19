using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Responses;

public record IngredientResponse(
    Guid Id,
    string Name,
    string Status)
{
    public static IngredientResponse FromDomain(Ingredient ingredient)
    {
        return new IngredientResponse(
            ingredient.Id.Value,
            ingredient.Name,
            ingredient.Status.ToString());
    }
}