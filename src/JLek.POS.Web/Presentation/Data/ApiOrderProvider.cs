namespace JLek.POS.Web.Presentation.Data;

public sealed class ApiOrderProvider
{
    private readonly Clients.Orders.IOrderClient _orderClient;
    private Guid? _activeOrderId;

    public ApiOrderProvider(Clients.Orders.IOrderClient orderClient)
    {
        _orderClient = orderClient;
    }

    public bool HasActiveOrder => _activeOrderId.HasValue;
    public Guid? ActiveOrderId => _activeOrderId;

    public async Task<Guid> EnsureOrderAsync(Guid tableId, CancellationToken cancellationToken = default)
    {
        if (_activeOrderId.HasValue)
            return _activeOrderId.Value;

        var response = await _orderClient.CreateAsync(tableId, cancellationToken);
        _activeOrderId = response.Id;
        return _activeOrderId.Value;
    }

    public async Task AddItemAsync(Guid orderId, Guid menuItemId, int quantity, decimal unitPrice, CancellationToken cancellationToken = default)
    {
        await _orderClient.AddItemAsync(orderId, menuItemId, quantity, unitPrice, cancellationToken);
    }

    public async Task ConfirmOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        await _orderClient.ConfirmAsync(orderId, cancellationToken);
    }

    public void ResetSession()
    {
        _activeOrderId = null;
    }
}
