namespace JLek.POS.Domain.Common.Abstractions;

public interface IClock
{
    DateTime UtcNow { get; }
    DateTime Today { get; }
}