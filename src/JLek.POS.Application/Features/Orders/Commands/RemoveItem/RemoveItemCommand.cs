using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Application.Features.Orders.Commands.RemoveItem;

public sealed record RemoveItemCommand(
    OrderId OrderId,
    OrderItemId OrderItemId);
