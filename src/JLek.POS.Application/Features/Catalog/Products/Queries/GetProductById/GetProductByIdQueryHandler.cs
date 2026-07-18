using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler
{
    private readonly IProductRepository _repository;

    public GetProductByIdQueryHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse?> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetByIdAsync(
            query.ProductId,
            cancellationToken);

        return product is not null
            ? ProductResponse.FromDomain(product)
            : null;
    }
}