using JLek.POS.Domain.Catalog.Events;
using JLek.POS.Domain.Catalog.Rules;
using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Common.ValueObjects;

namespace JLek.POS.Domain.Catalog;

public sealed class Product : AggregateRoot<ProductId>
{
    private readonly List<OptionGroup> _optionGroups = [];
    private readonly List<Modifier> _modifiers = [];
    private readonly List<Money> _suggestedPrices = [];
    private readonly List<IngredientId> _ingredientIds = [];

    private Product()
        : base(ProductId.From(Guid.Empty))
    {
        Name = string.Empty;
        CategoryId = ProductCategoryId.From(Guid.Empty);
    }

    private Product(
        ProductId id,
        string name,
        ProductCategoryId categoryId)
        : base(id)
    {
        Name = name;
        CategoryId = categoryId;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public ProductStatus Status { get; private set; }

    public ProductVisibility Visibility { get; private set; }

    public int? DisplayOrder { get; private set; }

    public ProductCategoryId CategoryId { get; private set; }

    public IReadOnlyCollection<OptionGroup> OptionGroups =>
        _optionGroups.AsReadOnly();

    public IReadOnlyCollection<Modifier> Modifiers =>
        _modifiers.AsReadOnly();

    public IReadOnlyCollection<Money> SuggestedPrices =>
        _suggestedPrices.AsReadOnly();

    public IReadOnlyCollection<IngredientId> IngredientIds =>
        _ingredientIds.AsReadOnly();

    public static Product Create(
        string name,
        ProductCategoryId categoryId)
    {
        var product = new Product(
            ProductId.New(),
            name,
            categoryId);

        product.RaiseDomainEvent(
            new ProductCreatedEvent(product.Id));

        return product;
    }

    public void UpdateDetails(
        string name,
        string? description,
        int? displayOrder)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        Name = name;
        Description = description;
        DisplayOrder = displayOrder;
    }

    public void ChangeCategory(ProductCategoryId categoryId)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        CategoryId = categoryId;
    }

    public void SetAvailability(ProductStatus status)
    {
        if (status == ProductStatus.Unavailable)
        {
            Status = ProductStatus.Unavailable;
            return;
        }

        // When transitioning to Available, validate every OptionGroup
        // has satisfiable selection rules
        foreach (var optionGroup in _optionGroups)
        {
            var rule = optionGroup.Rule;
            var availableOptionCount = optionGroup.Options.Count;

            if (rule.Min.HasValue && availableOptionCount < rule.Min.Value)
            {
                CheckRule(
                    new CannotSetAvailableWithUnsatisfiedSelectionRule(
                        optionGroup.Name,
                        rule.Min.Value,
                        availableOptionCount));
            }
        }

        Status = ProductStatus.Available;
    }

    public void SetVisibility(ProductVisibility visibility)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        Visibility = visibility;
    }

    public void AddSuggestedPrice(Money price)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        if (_suggestedPrices.Contains(price))
        {
            return;
        }

        _suggestedPrices.Add(price);
    }

    public void RemoveSuggestedPrice(Money price)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        _suggestedPrices.Remove(price);
    }

    public void AddOptionGroup(string name, SelectionRule rule)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        var optionGroup = OptionGroup.Create(name, rule);

        _optionGroups.Add(optionGroup);
    }

    public void RemoveOptionGroup(OptionGroupId optionGroupId)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        var optionGroup = _optionGroups.FirstOrDefault(
            x => x.Id == optionGroupId);

        if (optionGroup is not null)
        {
            _optionGroups.Remove(optionGroup);
        }
    }

    public void AddModifier(string name, decimal? priceAdjustment)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        var modifier = Modifier.Create(name, priceAdjustment);

        _modifiers.Add(modifier);
    }

    public void RemoveModifier(ModifierId modifierId)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        var modifier = _modifiers.FirstOrDefault(
            x => x.Id == modifierId);

        if (modifier is not null)
        {
            _modifiers.Remove(modifier);
        }
    }

    public void AddIngredient(IngredientId ingredientId)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        if (_ingredientIds.Contains(ingredientId))
        {
            return;
        }

        _ingredientIds.Add(ingredientId);
    }

    public void RemoveIngredient(IngredientId ingredientId)
    {
        CheckRule(new CannotModifyUnavailableProductRule(Status));

        _ingredientIds.Remove(ingredientId);
    }
}