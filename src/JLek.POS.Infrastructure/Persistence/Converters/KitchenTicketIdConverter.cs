using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class KitchenTicketIdConverter
    : StronglyTypedIdConverter<KitchenTicketId>
{
    public KitchenTicketIdConverter()
        : base(
            id => id.Value,
            KitchenTicketId.From)
    {
    }
}