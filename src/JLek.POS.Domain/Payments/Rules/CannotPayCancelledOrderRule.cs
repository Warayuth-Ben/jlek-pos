using JLek.POS.Domain.Common.Rules;
using JLek.POS.Domain.Orders;

namespace JLek.POS.Domain.Payments.Rules;

public sealed class CannotPayCancelledOrderRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public CannotPayCancelledOrderRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public string Message => "Cannot pay a cancelled order.";

    public bool IsBroken()
    {
        return _orderStatus == OrderStatus.Cancelled;
    }
}