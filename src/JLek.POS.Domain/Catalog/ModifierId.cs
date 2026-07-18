using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class ModifierId : ValueObject
{
    public Guid Value { get; }

    private ModifierId(Guid value)
    {
        Value = value;
    }

    public static ModifierId New()
    {
        return new(Guid.NewGuid());
    }

    public static ModifierId From(Guid value)
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