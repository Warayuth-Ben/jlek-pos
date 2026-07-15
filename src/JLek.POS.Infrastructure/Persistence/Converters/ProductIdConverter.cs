using JLek.POS.Domain.Catalog;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class ProductIdConverter
    : StronglyTypedIdConverter<ProductId>
{
    public ProductIdConverter()
        : base(
            id => id.Value,
            ProductId.From)
    {
    }
}