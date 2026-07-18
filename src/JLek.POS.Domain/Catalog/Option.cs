using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class Option : Entity<OptionId>
{
    private Option()
        : base(OptionId.From(Guid.Empty))
    {
        Name = string.Empty;
    }

    private Option(
        OptionId id,
        string name,
        decimal? priceAdjustment)
        : base(id)
    {
        Name = name;
        PriceAdjustment = priceAdjustment;
    }

    public string Name { get; private set; }

    public int? DisplayOrder { get; private set; }

    public OptionStatus Status { get; private set; }

    public ProductVisibility Visibility { get; private set; }

    /// <summary>
    /// Price adjustment amount. May be positive (increase price),
    /// negative (decrease price), or null (no price effect).
    /// </summary>
    public decimal? PriceAdjustment { get; private set; }

    internal static Option Create(
        string name,
        decimal? priceAdjustment)
    {
        return new Option(
            OptionId.New(),
            name,
            priceAdjustment);
    }

    public void Rename(string name)
    {
        Name = name;
    }

    public void SetDisplayOrder(int? displayOrder)
    {
        DisplayOrder = displayOrder;
    }

    public void SetStatus(OptionStatus status)
    {
        Status = status;
    }

    public void SetVisibility(ProductVisibility visibility)
    {
        Visibility = visibility;
    }

    public void ChangePriceAdjustment(decimal? priceAdjustment)
    {
        PriceAdjustment = priceAdjustment;
    }
}