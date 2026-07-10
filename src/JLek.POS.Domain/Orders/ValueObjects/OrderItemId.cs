using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Orders.ValueObjects;

public sealed class OrderItemId : ValueObject
{
    public Guid Value { get; }

    private OrderItemId(Guid value)
    {
        Value = value;
    }

    public static OrderItemId New()
    {
        return new(Guid.NewGuid());
    }

    public static OrderItemId From(Guid value)
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
