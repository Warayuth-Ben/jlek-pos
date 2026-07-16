using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Commands.HideProductCategory;

public sealed record HideProductCategoryCommand(
    ProductCategoryId ProductCategoryId);