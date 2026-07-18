using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.AddModifier;

public sealed class AddModifierCommandHandler
{
    private readonly IProductRepository _repository;

    public AddModifierCommandHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(
        AddModifierCommand command,
        CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetByIdAsync(
            command.ProductId,
            cancellationToken);

        if (product is null)
        {
            throw new InvalidOperationException("Product not found.");
        }

        product.AddModifier(command.Name, command.PriceAdjustment);

        await _repository.UpdateAsync(
            product,
            cancellationToken);

        return ProductResponse.FromDomain(product);
    }
}