using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Catalog;

public sealed class OptionGroup : Entity<OptionGroupId>
{
    private readonly List<Option> _options = [];

    private OptionGroup()
        : base(OptionGroupId.From(Guid.Empty))
    {
        Name = string.Empty;
        Rule = SelectionRule.Create(0, null);
    }

    private OptionGroup(
        OptionGroupId id,
        string name,
        SelectionRule rule)
        : base(id)
    {
        Name = name;
        Rule = rule;
    }

    public string Name { get; private set; }

    public int? DisplayOrder { get; private set; }

    public OptionGroupStatus Status { get; private set; }

    public ProductVisibility Visibility { get; private set; }

    public SelectionRule Rule { get; private set; }

    public IReadOnlyCollection<Option> Options =>
        _options.AsReadOnly();

    internal static OptionGroup Create(
        string name,
        SelectionRule rule)
    {
        return new OptionGroup(
            OptionGroupId.New(),
            name,
            rule);
    }

    public void Rename(string name)
    {
        Name = name;
    }

    public void SetDisplayOrder(int? displayOrder)
    {
        DisplayOrder = displayOrder;
    }

    public void SetStatus(OptionGroupStatus status)
    {
        Status = status;
    }

    public void SetVisibility(ProductVisibility visibility)
    {
        Visibility = visibility;
    }

    public void SetSelectionRule(SelectionRule rule)
    {
        Rule = rule;
    }

    public void AddOption(
        string name,
        decimal? priceAdjustment)
    {
        var option = Option.Create(name, priceAdjustment);

        _options.Add(option);
    }

    public void RemoveOption(OptionId optionId)
    {
        var option = _options.FirstOrDefault(
            x => x.Id == optionId);

        if (option is not null)
        {
            _options.Remove(option);
        }
    }
}