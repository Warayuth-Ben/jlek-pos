using JLek.POS.Domain.Payments;

namespace JLek.POS.Application.Abstractions.Repositories;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(
        PaymentId id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Payment>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Payment payment,
        CancellationToken cancellationToken = default);
}