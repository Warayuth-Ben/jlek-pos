using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler
{
    private readonly IProductRepository _repository;

    public GetProductsQueryHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ProductResponse>> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken = default)
    {
        var products = await _repository.GetAllAsync(
            cancellationToken);

        return products
            .Select(ProductResponse.FromDomain)
            .ToList();
    }
}