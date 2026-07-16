using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Responses;

public record ProductCategoryResponse(
    ProductCategoryId Id,
    string Name,
    int? DisplayOrder,
    ProductCategoryStatus Status)
{
    public static ProductCategoryResponse FromDomain(ProductCategory category)
    {
        return new ProductCategoryResponse(
            category.Id,
            category.Name,
            category.DisplayOrder,
            category.Status);
    }
}