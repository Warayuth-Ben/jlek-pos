using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Commands.HideProductCategory;

public sealed class HideProductCategoryCommandHandler
{
    private readonly IProductCategoryRepository _repository;

    public HideProductCategoryCommandHandler(
        IProductCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductCategoryResponse> Handle(
        HideProductCategoryCommand command,
        CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(
            command.ProductCategoryId,
            cancellationToken);

        if (category is null)
        {
            throw new InvalidOperationException("Product category not found.");
        }

        category.Hide();

        await _repository.UpdateAsync(
            category,
            cancellationToken);

        return ProductCategoryResponse.FromDomain(category);
    }
}