using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.ChangeCategory;

public sealed record ChangeCategoryCommand(
    ProductId ProductId,
    ProductCategoryId CategoryId);