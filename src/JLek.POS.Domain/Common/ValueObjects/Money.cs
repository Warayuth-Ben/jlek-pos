using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Common.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        Amount = amount;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
    }

    public static Money Zero => new(0m);

    public static Money operator +(Money left, Money right)
    {
        return new(left.Amount + right.Amount);
    }

    public static Money operator -(Money left, Money right)
    {
        return new(left.Amount - right.Amount);
    }
}
