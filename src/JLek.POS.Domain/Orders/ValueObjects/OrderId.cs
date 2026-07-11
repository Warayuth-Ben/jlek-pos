using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Orders.ValueObjects;

public sealed class OrderId : ValueObject
{
    public Guid Value { get; }

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId New()
    {
        return new(Guid.NewGuid());
    }

    public static OrderId From(Guid value)
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