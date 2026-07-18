using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Payments;

public sealed class PaymentId : ValueObject
{
    public Guid Value { get; }

    private PaymentId(Guid value)
    {
        Value = value;
    }

    public static PaymentId New()
    {
        return new(Guid.NewGuid());
    }

    public static PaymentId From(Guid value)
    {
        return new(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}