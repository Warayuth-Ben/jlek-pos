using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Commands.ReorderProductCategory;

public sealed class ReorderProductCategoryCommandHandler
{
    private readonly IProductCategoryRepository _repository;

    public ReorderProductCategoryCommandHandler(
        IProductCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductCategoryResponse> Handle(
        ReorderProductCategoryCommand command,
        CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(
            command.ProductCategoryId,
            cancellationToken);

        if (category is null)
        {
            throw new InvalidOperationException("Product category not found.");
        }

        category.Reorder(command.DisplayOrder);

        await _repository.UpdateAsync(
            category,
            cancellationToken);

        return ProductCategoryResponse.FromDomain(category);
    }
}