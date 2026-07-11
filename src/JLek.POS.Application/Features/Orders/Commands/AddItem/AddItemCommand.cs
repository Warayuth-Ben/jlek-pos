using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Application.Features.Orders.Commands.AddItem;

public sealed record AddItemCommand(
    OrderId OrderId,
    Guid MenuItemId,
    int Quantity,
    decimal UnitPrice);