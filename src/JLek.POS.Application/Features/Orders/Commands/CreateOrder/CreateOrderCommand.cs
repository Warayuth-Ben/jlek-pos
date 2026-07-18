using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Features.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    TableId TableId);