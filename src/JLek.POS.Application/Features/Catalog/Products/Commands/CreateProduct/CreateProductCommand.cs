using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    ProductCategoryId CategoryId);