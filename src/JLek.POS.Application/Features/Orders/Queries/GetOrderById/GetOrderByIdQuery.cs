using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Application.Features.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(
    OrderId OrderId);