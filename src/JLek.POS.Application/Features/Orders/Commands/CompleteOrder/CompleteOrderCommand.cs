using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Application.Features.Orders.Commands.CompleteOrder;

public sealed record CompleteOrderCommand(
    OrderId OrderId);