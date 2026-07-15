using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class IngredientId : ValueObject
{
    public Guid Value { get; }

    private IngredientId(Guid value)
    {
        Value = value;
    }

    public static IngredientId New()
    {
        return new(Guid.NewGuid());
    }

    public static IngredientId From(Guid value)
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