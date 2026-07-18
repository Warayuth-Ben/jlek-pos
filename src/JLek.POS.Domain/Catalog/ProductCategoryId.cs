using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class ProductCategoryId : ValueObject
{
    public Guid Value { get; }

    private ProductCategoryId(Guid value)
    {
        Value = value;
    }

    public static ProductCategoryId New()
    {
        return new(Guid.NewGuid());
    }

    public static ProductCategoryId From(Guid value)
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