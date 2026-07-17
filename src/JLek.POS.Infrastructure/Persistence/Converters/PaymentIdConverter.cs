using JLek.POS.Domain.Payments;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class PaymentIdConverter
    : StronglyTypedIdConverter<PaymentId>
{
    public PaymentIdConverter()
        : base(
            id => id.Value,
            PaymentId.From)
    {
    }
}