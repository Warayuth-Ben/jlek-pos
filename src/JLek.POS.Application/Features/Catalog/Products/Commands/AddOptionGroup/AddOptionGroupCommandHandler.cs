using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.AddOptionGroup;

public sealed class AddOptionGroupCommandHandler
{
    private readonly IProductRepository _repository;

    public AddOptionGroupCommandHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(
        AddOptionGroupCommand command,
        CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetByIdAsync(
            command.ProductId,
            cancellationToken);

        if (product is null)
        {
            throw new InvalidOperationException("Product not found.");
        }

        product.AddOptionGroup(command.Name, command.Rule);

        await _repository.UpdateAsync(
            product,
            cancellationToken);

        return ProductResponse.FromDomain(product);
    }
}