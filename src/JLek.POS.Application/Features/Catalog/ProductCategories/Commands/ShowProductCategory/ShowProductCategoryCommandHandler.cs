using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.ProductCategories.Commands.ShowProductCategory;

public sealed class ShowProductCategoryCommandHandler
{
    private readonly IProductCategoryRepository _repository;

    public ShowProductCategoryCommandHandler(
        IProductCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductCategoryResponse> Handle(
        ShowProductCategoryCommand command,
        CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(
            command.ProductCategoryId,
            cancellationToken);

        if (category is null)
        {
            throw new InvalidOperationException("Product category not found.");
        }

        category.Show();

        await _repository.UpdateAsync(
            category,
            cancellationToken);

        return ProductCategoryResponse.FromDomain(category);
    }
}