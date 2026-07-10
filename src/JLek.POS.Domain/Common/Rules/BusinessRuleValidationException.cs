namespace JLek.POS.Domain.Common.Rules;

public sealed class BusinessRuleValidationException : Exception
{
    public BusinessRuleValidationException(IBusinessRule rule)
        : base(rule.Message)
    {
    }
}
