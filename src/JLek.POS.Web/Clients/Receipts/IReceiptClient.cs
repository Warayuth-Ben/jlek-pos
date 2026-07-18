namespace JLek.POS.Web.Clients.Receipts;

public interface IReceiptClient
{
    Task PrintCustomerReceiptAsync(Guid orderId, CancellationToken ct = default);
}