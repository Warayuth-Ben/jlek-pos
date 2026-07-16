using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Commands.RenameProductCategory;

public sealed record RenameProductCategoryCommand(
    ProductCategoryId ProductCategoryId,
    string Name);