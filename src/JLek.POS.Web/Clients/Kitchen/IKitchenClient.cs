using JLek.POS.Web.Contracts.Kitchen;

namespace JLek.POS.Web.Clients.Kitchen;

public interface IKitchenClient
{
    Task<List<KitchenTicketResponse>> GetActiveAsync(CancellationToken ct = default);
    Task<List<KitchenTicketResponse>> GetAllAsync(CancellationToken ct = default);
    Task StartPreparationAsync(Guid id, CancellationToken ct = default);
    Task CompletePreparationAsync(Guid id, CancellationToken ct = default);
    Task ServeAsync(Guid id, CancellationToken ct = default);
}