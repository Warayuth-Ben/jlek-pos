using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Catalog.Rules;

public sealed class CannotSetAvailableWithUnsatisfiedSelectionRule : IBusinessRule
{
    private readonly string _optionGroupName;
    private readonly int _requiredMin;
    private readonly int _availableCount;

    public CannotSetAvailableWithUnsatisfiedSelectionRule(
        string optionGroupName,
        int requiredMin,
        int availableCount)
    {
        _optionGroupName = optionGroupName;
        _requiredMin = requiredMin;
        _availableCount = availableCount;
    }

    public bool IsBroken()
    {
        return true;
    }

    public string Message =>
        $"Cannot set product to Available: OptionGroup '{_optionGroupName}' " +
        $"requires a minimum of {_requiredMin} selection(s), " +
        $"but only {_availableCount} option(s) are available.";
}