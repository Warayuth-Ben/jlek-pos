using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public abstract class StronglyTypedIdConverter<TId>
    : ValueConverter<TId, Guid>
    where TId : class
{
    protected StronglyTypedIdConverter(
        Func<TId, Guid> toProvider,
        Func<Guid, TId> fromProvider)
        : base(
            id => toProvider(id),
            value => fromProvider(value))
    {
    }
}