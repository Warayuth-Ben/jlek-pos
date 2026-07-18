using JLek.POS.Domain.Catalog;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class IngredientIdConverter
    : StronglyTypedIdConverter<IngredientId>
{
    public IngredientIdConverter()
        : base(
            id => id.Value,
            IngredientId.From)
    {
    }
}