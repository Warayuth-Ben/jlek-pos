using JLek.POS.Domain.Catalog;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class OptionGroupIdConverter
    : StronglyTypedIdConverter<OptionGroupId>
{
    public OptionGroupIdConverter()
        : base(
            id => id.Value,
            OptionGroupId.From)
    {
    }
}