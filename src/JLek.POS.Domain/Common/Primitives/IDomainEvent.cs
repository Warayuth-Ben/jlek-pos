namespace JLek.POS.Domain.Common.Primitives;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}
