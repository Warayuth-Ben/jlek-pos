using JLek.POS.Api.Requests;
using JLek.POS.Api.Responses;
using JLek.POS.Application.Features.Orders.Commands.AddItem;
using JLek.POS.Application.Features.Orders.Commands.CreateOrder;
using JLek.POS.Application.Features.Orders.Queries.GetOrderById;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Api.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/orders");

        group.MapPost("/", async (
            CreateOrderRequest request,
            CreateOrderCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var order = await handler.Handle(
                new CreateOrderCommand(),
                cancellationToken);

            var response = order.ToResponse();

            return Results.Created(
                $"/orders/{response.Id}",
                response);
        });

        group.MapGet("/{id:guid}", async (
            Guid id,
            GetOrderByIdQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var order = await handler.Handle(
                new GetOrderByIdQuery(
                    OrderId.From(id)),
                cancellationToken);

            if (order is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(order.ToResponse());
        });

        group.MapPost("/{id:guid}/items", async (
            Guid id,
            AddItemRequest request,
            AddItemCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var order = await handler.Handle(
                new AddItemCommand(
                    OrderId.From(id),
                    request.MenuItemId,
                    request.Quantity,
                    request.UnitPrice),
                cancellationToken);

            return Results.Ok(order.ToResponse());
        });

        return app;
    }
}
