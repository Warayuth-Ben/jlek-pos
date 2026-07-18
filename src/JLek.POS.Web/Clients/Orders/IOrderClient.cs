using JLek.POS.Web.Contracts.Orders;

namespace JLek.POS.Web.Clients.Orders;

public interface IOrderClient
{
    Task<OrderResponse> CreateAsync(Guid tableId, CancellationToken ct = default);
    Task<OrderResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<OrderResponse> AddItemAsync(Guid orderId, Guid menuItemId, int quantity, decimal unitPrice, CancellationToken ct = default);
    Task<OrderResponse> ConfirmAsync(Guid orderId, CancellationToken ct = default);
}