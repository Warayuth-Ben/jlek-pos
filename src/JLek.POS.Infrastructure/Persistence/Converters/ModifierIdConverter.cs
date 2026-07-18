using JLek.POS.Domain.Catalog;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class ModifierIdConverter
    : StronglyTypedIdConverter<ModifierId>
{
    public ModifierIdConverter()
        : base(
            id => id.Value,
            ModifierId.From)
    {
    }
}