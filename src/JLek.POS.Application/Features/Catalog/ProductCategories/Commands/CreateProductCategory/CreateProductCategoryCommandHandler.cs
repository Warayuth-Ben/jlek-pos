using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;
using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Commands.CreateProductCategory;

public sealed class CreateProductCategoryCommandHandler
{
    private readonly IProductCategoryRepository _repository;

    public CreateProductCategoryCommandHandler(
        IProductCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductCategoryResponse> Handle(
        CreateProductCategoryCommand command,
        CancellationToken cancellationToken = default)
    {
        var category = ProductCategory.Create(command.Name);

        await _repository.AddAsync(
            category,
            cancellationToken);

        return ProductCategoryResponse.FromDomain(category);
    }
}