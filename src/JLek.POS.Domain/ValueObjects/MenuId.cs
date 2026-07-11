namespace JLek.POS.Domain.ValueObjects;

public sealed record MenuId(Guid Value)
{
    public static MenuId New() => new(Guid.NewGuid());

    public static MenuId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("MenuId cannot be empty.", nameof(value));
        }

        return new(value);
    }

    public override string ToString() => Value.ToString();
}