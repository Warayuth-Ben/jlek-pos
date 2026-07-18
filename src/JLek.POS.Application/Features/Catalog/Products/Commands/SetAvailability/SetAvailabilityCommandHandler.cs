using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.SetAvailability;

public sealed class SetAvailabilityCommandHandler
{
    private readonly IProductRepository _repository;

    public SetAvailabilityCommandHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(
        SetAvailabilityCommand command,
        CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetByIdAsync(
            command.ProductId,
            cancellationToken);

        if (product is null)
        {
            throw new InvalidOperationException("Product not found.");
        }

        product.SetAvailability(command.Status);

        await _repository.UpdateAsync(
            product,
            cancellationToken);

        return ProductResponse.FromDomain(product);
    }
}