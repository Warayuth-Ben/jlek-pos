using JLek.POS.Domain.Common.Rules;
using JLek.POS.Domain.Orders;

namespace JLek.POS.Domain.Payments.Rules;

public sealed class CannotPayCompletedOrderRule : IBusinessRule
{
    private readonly OrderStatus _orderStatus;

    public CannotPayCompletedOrderRule(OrderStatus orderStatus)
    {
        _orderStatus = orderStatus;
    }

    public string Message => "Cannot pay a completed order.";

    public bool IsBroken()
    {
        return _orderStatus == OrderStatus.Completed;
    }
}