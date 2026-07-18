using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.SetVisibility;

public sealed class SetVisibilityCommandHandler
{
    private readonly IProductRepository _repository;

    public SetVisibilityCommandHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(
        SetVisibilityCommand command,
        CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetByIdAsync(
            command.ProductId,
            cancellationToken);

        if (product is null)
        {
            throw new InvalidOperationException("Product not found.");
        }

        product.SetVisibility(command.Visibility);

        await _repository.UpdateAsync(
            product,
            cancellationToken);

        return ProductResponse.FromDomain(product);
    }
}