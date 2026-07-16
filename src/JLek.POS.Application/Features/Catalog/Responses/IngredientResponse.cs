using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Responses;

public record IngredientResponse(
    IngredientId Id,
    string Name,
    IngredientStatus Status)
{
    public static IngredientResponse FromDomain(Ingredient ingredient)
    {
        return new IngredientResponse(
            ingredient.Id,
            ingredient.Name,
            ingredient.Status);
    }
}