using JLek.POS.Domain.Catalog;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class OptionIdConverter
    : StronglyTypedIdConverter<OptionId>
{
    public OptionIdConverter()
        : base(
            id => id.Value,
            OptionId.From)
    {
    }
}