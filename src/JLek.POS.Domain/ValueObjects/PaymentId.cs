namespace JLek.POS.Domain.ValueObjects;

public sealed record PaymentId(Guid Value)
{
    public static PaymentId New() => new(Guid.NewGuid());

    public static PaymentId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("PaymentId cannot be empty.", nameof(value));
        }

        return new(value);
    }

    public override string ToString() => Value.ToString();
}