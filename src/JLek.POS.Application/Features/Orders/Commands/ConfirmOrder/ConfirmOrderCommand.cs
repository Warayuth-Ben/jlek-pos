using JLek.POS.Application.Abstractions;

namespace JLek.POS.Application.Features.Orders.Commands.ConfirmOrder;

public sealed record ConfirmOrderCommand(
    Guid OrderId) : ICommand;