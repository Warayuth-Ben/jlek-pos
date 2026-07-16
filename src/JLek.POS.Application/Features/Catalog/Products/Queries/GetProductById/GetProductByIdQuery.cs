using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(
    ProductId ProductId);