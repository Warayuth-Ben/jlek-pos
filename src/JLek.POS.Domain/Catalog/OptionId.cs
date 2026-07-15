using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class OptionId : ValueObject
{
    public Guid Value { get; }

    private OptionId(Guid value)
    {
        Value = value;
    }

    public static OptionId New()
    {
        return new(Guid.NewGuid());
    }

    public static OptionId From(Guid value)
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