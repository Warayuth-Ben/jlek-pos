using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.RemoveIngredient;

public sealed class RemoveIngredientCommandHandler
{
    private readonly IProductRepository _repository;

    public RemoveIngredientCommandHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(
        RemoveIngredientCommand command,
        CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetByIdAsync(
            command.ProductId,
            cancellationToken);

        if (product is null)
        {
            throw new InvalidOperationException("Product not found.");
        }

        product.RemoveIngredient(command.IngredientId);

        await _repository.UpdateAsync(
            product,
            cancellationToken);

        return ProductResponse.FromDomain(product);
    }
}