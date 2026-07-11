using JLek.POS.Application.Abstractions;

namespace JLek.POS.Application.Features.Orders.Commands.CompleteOrder;

public sealed record CompleteOrderCommand(
    Guid OrderId) : ICommand;