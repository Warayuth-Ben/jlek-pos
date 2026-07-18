using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Queries.GetProductCategoryById;

public sealed class GetProductCategoryByIdQueryHandler
{
    private readonly IProductCategoryRepository _repository;

    public GetProductCategoryByIdQueryHandler(
        IProductCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductCategoryResponse?> Handle(
        GetProductCategoryByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(
            query.ProductCategoryId,
            cancellationToken);

        return category is not null
            ? ProductCategoryResponse.FromDomain(category)
            : null;
    }
}