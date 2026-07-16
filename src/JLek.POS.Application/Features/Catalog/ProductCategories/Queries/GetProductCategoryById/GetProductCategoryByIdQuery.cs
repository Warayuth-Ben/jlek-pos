using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Queries.GetProductCategoryById;

public sealed record GetProductCategoryByIdQuery(
    ProductCategoryId ProductCategoryId);