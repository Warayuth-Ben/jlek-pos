using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Responses;

public record KitchenItemResponse(
    KitchenItemId Id,
    string ItemName,
    int Quantity,
    string? Notes)
{
    public static KitchenItemResponse FromDomain(KitchenItem item)
    {
        return new KitchenItemResponse(
            item.Id,
            item.ItemName,
            item.Quantity,
            item.Notes);
    }
}