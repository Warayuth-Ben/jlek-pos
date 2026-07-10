using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Common.ValueObjects;

public sealed class Quantity : ValueObject
{
    public int Value { get; }

    public Quantity(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Quantity Zero => new(0);

    public static Quantity operator +(Quantity left, Quantity right)
    {
        return new(left.Value + right.Value);
    }

    public static Quantity operator -(Quantity left, Quantity right)
    {
        return new(left.Value - right.Value);
    }
}
