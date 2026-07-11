using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Application.Features.Orders.Commands.ConfirmOrder;

public sealed record ConfirmOrderCommand(
    OrderId OrderId);