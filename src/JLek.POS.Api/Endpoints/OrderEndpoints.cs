using JLek.POS.Api.Requests;
using JLek.POS.Api.Responses;
using JLek.POS.Application.Features.Orders.Commands.AddItem;
using JLek.POS.Application.Features.Orders.Commands.CancelOrder;
using JLek.POS.Application.Features.Orders.Commands.CompleteOrder;
using JLek.POS.Application.Features.Orders.Commands.ConfirmOrder;
using JLek.POS.Application.Features.Orders.Commands.CreateOrder;
using JLek.POS.Application.Features.Orders.Commands.RemoveItem;
using JLek.POS.Application.Features.Orders.Queries.GetOrderById;
using JLek.POS.Application.Features.Orders.Queries.GetOrders;
using JLek.POS.Domain.Orders.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace JLek.POS.Api.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/orders");

        group.MapPost("/", async (
            CreateOrderRequest request,
            [FromServices] CreateOrderCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var order = await handler.Handle(
                new CreateOrderCommand(),
                cancellationToken);

            return Results.Created(
                $"/orders/{order.Id}",
                order.ToResponse());
        });

        group.MapGet("/", async (
            [FromServices] GetOrdersQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var orders = await handler.Handle(
                new GetOrdersQuery(),
                cancellationToken);

            return Results.Ok(
                orders.Select(x => x.ToResponse()));
        });

        group.MapGet("/{id:guid}", async (
            Guid id,
            [FromServices] GetOrderByIdQueryHandler handler,
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
            [FromServices] AddItemCommandHandler handler,
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

        group.MapDelete("/{id:guid}/items/{itemId:guid}", async (
            Guid id,
            Guid itemId,
            [FromServices] RemoveItemCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var order = await handler.Handle(
                new RemoveItemCommand(
                    OrderId.From(id),
                    OrderItemId.From(itemId)),
                cancellationToken);

            return Results.Ok(order.ToResponse());
        });

        group.MapPost("/{id:guid}/confirm", async (
            Guid id,
            [FromServices] ConfirmOrderCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var order = await handler.Handle(
                new ConfirmOrderCommand(
                    OrderId.From(id)),
                cancellationToken);

            return Results.Ok(order.ToResponse());
        });

        group.MapPost("/{id:guid}/complete", async (
            Guid id,
            [FromServices] CompleteOrderCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var order = await handler.Handle(
                new CompleteOrderCommand(
                    OrderId.From(id)),
                cancellationToken);

            return Results.Ok(order.ToResponse());
        });

        group.MapPost("/{id:guid}/cancel", async (
            Guid id,
            [FromServices] CancelOrderCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var order = await handler.Handle(
                new CancelOrderCommand(
                    OrderId.From(id)),
                cancellationToken);

            return Results.Ok(order.ToResponse());
        });

        return app;
    }
}
