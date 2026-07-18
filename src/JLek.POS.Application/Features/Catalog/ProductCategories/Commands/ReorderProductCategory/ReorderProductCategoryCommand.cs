using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Commands.ReorderProductCategory;

public sealed record ReorderProductCategoryCommand(
    ProductCategoryId ProductCategoryId,
    int? DisplayOrder);