using JLek.POS.Web.Contracts.Payments;

namespace JLek.POS.Web.Clients.Payments;

public interface IPaymentClient
{
    Task<PaymentResponse> ReceivePaymentAsync(Guid orderId, decimal amount, string method, CancellationToken ct = default);
}