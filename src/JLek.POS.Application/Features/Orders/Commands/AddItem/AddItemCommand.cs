using JLek.POS.Application.Abstractions;

namespace JLek.POS.Application.Features.Orders.Commands.AddItem;

public sealed record AddItemCommand(
    Guid OrderId,
    Guid MenuItemId,
    int Quantity,
    decimal UnitPrice) : ICommand;