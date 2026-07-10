using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Common.ValueObjects;

public sealed class Quantity : ValueObject
{
    public int Value { get; }

    private Quantity(int value)
    {
        Value = value;
    }

    public static Quantity Zero => new(0);

    public static Quantity From(int value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new(value);
    }

    public static Quantity operator +(Quantity left, Quantity right)
    {
        return new(left.Value + right.Value);
    }

    public static Quantity operator -(Quantity left, Quantity right)
    {
        return new(left.Value - right.Value);
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
