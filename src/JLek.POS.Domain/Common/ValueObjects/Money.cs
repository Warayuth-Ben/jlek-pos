using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Common.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Amount { get; }

    private Money(decimal amount)
    {
        Amount = amount;
    }

    public static Money Zero => new(0m);

    public static Money From(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        return new(amount);
    }

    public static Money operator +(Money left, Money right)
    {
        return new(left.Amount + right.Amount);
    }

    public static Money operator -(Money left, Money right)
    {
        return new(left.Amount - right.Amount);
    }

    public static Money operator *(Money money, int quantity)
    {
        return new(money.Amount * quantity);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
    }

    public override string ToString()
    {
        return Amount.ToString("0.00");
    }
}
