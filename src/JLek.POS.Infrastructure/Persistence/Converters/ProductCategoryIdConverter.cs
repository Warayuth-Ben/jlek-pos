using JLek.POS.Domain.Catalog;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class ProductCategoryIdConverter
    : StronglyTypedIdConverter<ProductCategoryId>
{
    public ProductCategoryIdConverter()
        : base(
            id => id.Value,
            ProductCategoryId.From)
    {
    }
}