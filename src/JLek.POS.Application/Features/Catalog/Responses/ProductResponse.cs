using JLek.POS.Domain.Catalog;
using JLek.POS.Domain.Common.ValueObjects;

namespace JLek.POS.Application.Features.Catalog.Responses;

public record ProductResponse(
    Guid Id,
    string Name,
    string? Description,
    string Status,
    string Visibility,
    int? DisplayOrder,
    Guid CategoryId,
    List<object> OptionGroups,
    List<object> Modifiers,
    List<decimal> SuggestedPrices,
    List<Guid> IngredientIds)
{
    public static ProductResponse FromDomain(Product product)
    {
        return new ProductResponse(
            product.Id.Value,
            product.Name,
            product.Description,
            product.Status.ToString(),
            product.Visibility.ToString(),
            product.DisplayOrder,
            product.CategoryId.Value,
            product.OptionGroups.Cast<object>().ToList(),
            product.Modifiers.Cast<object>().ToList(),
            product.SuggestedPrices.Select(m => m.Amount).ToList(),
            product.IngredientIds.Select(id => id.Value).ToList());
    }
}