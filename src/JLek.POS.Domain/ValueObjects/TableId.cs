namespace JLek.POS.Domain.ValueObjects;

public sealed record TableId(Guid Value)
{
    public static TableId New() => new(Guid.NewGuid());

    public static TableId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("TableId cannot be empty.", nameof(value));
        }

        return new(value);
    }

    public override string ToString() => Value.ToString();
}