using JLek.POS.Domain.Common.Abstractions;

namespace JLek.POS.Infrastructure.Time;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Today => DateTime.UtcNow.Date;
}