namespace JLek.POS.Domain.Kitchen;

public sealed record KitchenItemId(Guid Value)
{
    public static KitchenItemId New() => new(Guid.NewGuid());

    public static KitchenItemId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("KitchenItemId cannot be empty.", nameof(value));
        }

        return new(value);
    }

    public override string ToString() => Value.ToString();
}