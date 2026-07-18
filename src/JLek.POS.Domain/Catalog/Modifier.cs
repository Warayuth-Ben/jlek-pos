using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class Modifier : Entity<ModifierId>
{
    private Modifier()
        : base(ModifierId.From(Guid.Empty))
    {
        Name = string.Empty;
    }

    private Modifier(
        ModifierId id,
        string name,
        decimal? priceAdjustment)
        : base(id)
    {
        Name = name;
        PriceAdjustment = priceAdjustment;
    }

    public string Name { get; private set; }

    public ModifierStatus Status { get; private set; }

    public ProductVisibility Visibility { get; private set; }

    /// <summary>
    /// Price adjustment amount. May be positive (increase price),
    /// negative (decrease price), or null (no price effect).
    /// </summary>
    public decimal? PriceAdjustment { get; private set; }

    internal static Modifier Create(
        string name,
        decimal? priceAdjustment)
    {
        return new Modifier(
            ModifierId.New(),
            name,
            priceAdjustment);
    }

    public void Rename(string name)
    {
        Name = name;
    }

    public void SetStatus(ModifierStatus status)
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