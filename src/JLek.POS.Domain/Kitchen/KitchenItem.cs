using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Kitchen;

public sealed class KitchenItem : Entity<KitchenItemId>
{
    private KitchenItem()
        : base(KitchenItemId.From(Guid.Empty))
    {
        ItemName = string.Empty;
    }

    private KitchenItem(
        KitchenItemId id,
        string itemName,
        int quantity,
        string? notes)
        : base(id)
    {
        ItemName = itemName;
        Quantity = quantity;
        Notes = notes;
    }

    public string ItemName { get; private set; }

    public int Quantity { get; private set; }

    public string? Notes { get; private set; }

    internal static KitchenItem Create(
        string itemName,
        int quantity,
        string? notes)
    {
        return new KitchenItem(
            KitchenItemId.New(),
            itemName,
            quantity,
            notes);
    }
}