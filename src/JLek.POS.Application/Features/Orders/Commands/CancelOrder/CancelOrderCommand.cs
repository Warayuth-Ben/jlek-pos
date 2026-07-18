using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Application.Features.Orders.Commands.CancelOrder;

public sealed record CancelOrderCommand(
    OrderId OrderId);