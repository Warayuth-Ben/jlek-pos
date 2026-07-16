using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Catalog.Responses;
using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Features.Catalog.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken = default)
    {
        var product = Product.Create(
            command.Name,
            command.CategoryId);

        await _repository.AddAsync(
            product,
            cancellationToken);

        return ProductResponse.FromDomain(product);
    }
}