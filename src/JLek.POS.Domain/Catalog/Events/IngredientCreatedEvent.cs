using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog.Events;

public sealed class IngredientCreatedEvent : DomainEvent
{
    public IngredientCreatedEvent(IngredientId ingredientId)
    {
        IngredientId = ingredientId;
    }

    public IngredientId IngredientId { get; }
}