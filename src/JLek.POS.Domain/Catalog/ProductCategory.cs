using JLek.POS.Domain.Catalog.Events;
using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class ProductCategory : AggregateRoot<ProductCategoryId>
{
    private ProductCategory()
        : base(ProductCategoryId.From(Guid.Empty))
    {
        Name = string.Empty;
    }

    private ProductCategory(
        ProductCategoryId id,
        string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public int? DisplayOrder { get; private set; }

    public ProductCategoryStatus Status { get; private set; }

    public static ProductCategory Create(string name)
    {
        var category = new ProductCategory(
            ProductCategoryId.New(),
            name);

        category.RaiseDomainEvent(
            new ProductCategoryCreatedEvent(category.Id));

        return category;
    }

    public void Rename(string name)
    {
        Name = name;
    }

    public void Reorder(int? displayOrder)
    {
        DisplayOrder = displayOrder;
    }

    public void Hide()
    {
        Status = ProductCategoryStatus.Hidden;
    }

    public void Show()
    {
        Status = ProductCategoryStatus.Available;
    }
}