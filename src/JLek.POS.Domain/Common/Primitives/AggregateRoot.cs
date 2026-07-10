using System.Collections.ObjectModel;
using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Common.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents =>
        new ReadOnlyCollection<IDomainEvent>(_domainEvents);

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
