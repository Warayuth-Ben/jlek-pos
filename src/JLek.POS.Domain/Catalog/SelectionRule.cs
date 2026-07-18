using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class SelectionRule : ValueObject
{
    public int? Min { get; }
    public int? Max { get; }

    private SelectionRule(int? min, int? max)
    {
        Min = min;
        Max = max;
    }

    public static SelectionRule Create(int? min, int? max)
    {
        if (min.HasValue && min.Value < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(min),
                "Minimum selection must be zero or greater.");
        }

        if (max.HasValue && max.Value < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(max),
                "Maximum selection must be zero or greater.");
        }

        if (min.HasValue && max.HasValue && min.Value > max.Value)
        {
            throw new ArgumentOutOfRangeException(
                nameof(min),
                "Minimum selection cannot exceed maximum selection.");
        }

        return new SelectionRule(min, max);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Min;
        yield return Max;
    }

    public override string ToString()
    {
        if (Max is null)
        {
            return $"Min: {Min ?? 0}, Max: Unlimited";
        }

        return $"Min: {Min ?? 0}, Max: {Max}";
    }
}