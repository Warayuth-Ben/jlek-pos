namespace JLek.POS.Domain.Common.Rules;

public interface IBusinessRule
{
    bool IsBroken();

    string Message { get; }
}
