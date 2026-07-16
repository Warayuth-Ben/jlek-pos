using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class KitchenItemIdConverter
    : StronglyTypedIdConverter<KitchenItemId>
{
    public KitchenItemIdConverter()
        : base(
            id => id.Value,
            KitchenItemId.From)
    {
    }
}