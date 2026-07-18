namespace JLek.POS.Application.Abstractions.EventHandling;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(
        IReadOnlyCollection<Domain.Common.Primitives.IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default);
}