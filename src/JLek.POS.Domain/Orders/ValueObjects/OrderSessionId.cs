using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Orders.ValueObjects;

public sealed record OrderSessionId(Guid Value)
{
    public static OrderSessionId New()
        => new(Guid.NewGuid());

    public override string ToString()
        => Value.ToString();
}
