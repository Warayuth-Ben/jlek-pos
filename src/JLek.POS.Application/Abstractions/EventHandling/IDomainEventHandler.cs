using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Application.Abstractions.EventHandling;

public interface IDomainEventHandler<TEvent>
    where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken = default);
}