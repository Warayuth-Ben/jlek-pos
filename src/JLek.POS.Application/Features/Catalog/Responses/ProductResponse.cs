using JLek.POS.Domain.Catalog;
using JLek.POS.Domain.Common.ValueObjects;

namespace JLek.POS.Application.Features.Catalog.Responses;

public record ProductResponse(
    ProductId Id,
    string Name,
    string? Description,
    ProductStatus Status,
    ProductVisibility Visibility,
    int? DisplayOrder,
    ProductCategoryId CategoryId,
    IReadOnlyCollection<OptionGroup> OptionGroups,
    IReadOnlyCollection<Modifier> Modifiers,
    IReadOnlyCollection<Money> SuggestedPrices,
    IReadOnlyCollection<IngredientId> IngredientIds)
{
    public static ProductResponse FromDomain(Product product)
    {
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Status,
            product.Visibility,
            product.DisplayOrder,
            product.CategoryId,
            product.OptionGroups,
            product.Modifiers,
            product.SuggestedPrices,
            product.IngredientIds);
    }
}