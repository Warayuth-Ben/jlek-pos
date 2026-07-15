using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog.Events;

public sealed class ProductCreatedEvent : DomainEvent
{
    public ProductCreatedEvent(ProductId productId)
    {
        ProductId = productId;
    }

    public ProductId ProductId { get; }
}