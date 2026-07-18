using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Queries.GetProductCategories;

public sealed class GetProductCategoriesQueryHandler
{
    private readonly IProductCategoryRepository _repository;

    public GetProductCategoriesQueryHandler(
        IProductCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ProductCategoryResponse>> Handle(
        GetProductCategoriesQuery query,
        CancellationToken cancellationToken = default)
    {
        var categories = await _repository.GetAllAsync(
            cancellationToken);

        return categories
            .Select(ProductCategoryResponse.FromDomain)
            .ToList();
    }
}