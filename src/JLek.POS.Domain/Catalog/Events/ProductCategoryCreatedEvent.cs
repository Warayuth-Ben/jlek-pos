using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog.Events;

public sealed class ProductCategoryCreatedEvent : DomainEvent
{
    public ProductCategoryCreatedEvent(ProductCategoryId categoryId)
    {
        CategoryId = categoryId;
    }

    public ProductCategoryId CategoryId { get; }
}