using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Commands.ShowProductCategory;

public sealed record ShowProductCategoryCommand(
    ProductCategoryId ProductCategoryId);