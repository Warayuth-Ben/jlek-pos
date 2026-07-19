using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Responses;

public record ProductCategoryResponse(
    Guid Id,
    string Name,
    int? DisplayOrder,
    string Status)
{
    public static ProductCategoryResponse FromDomain(ProductCategory category)
    {
        return new ProductCategoryResponse(
            category.Id.Value,
            category.Name,
            category.DisplayOrder,
            category.Status.ToString());
    }
}

public record ProductCategoryStatusValue(Guid Id, string Name, int? DisplayOrder, string Status);