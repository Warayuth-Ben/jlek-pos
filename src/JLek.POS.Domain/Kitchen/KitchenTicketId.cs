using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Kitchen;

public sealed record KitchenTicketId(Guid Value)
{
    public static KitchenTicketId New() => new(Guid.NewGuid());

    public static KitchenTicketId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("KitchenTicketId cannot be empty.", nameof(value));
        }

        return new(value);
    }

    public override string ToString() => Value.ToString();
}