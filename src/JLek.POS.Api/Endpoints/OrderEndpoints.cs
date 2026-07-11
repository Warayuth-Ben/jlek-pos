using JLek.POS.Api.Requests;
using JLek.POS.Application.Features.Orders.Commands.CreateOrder;

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

            return Results.Created(
                $"/orders/{order.Id}",
                order);
        });

        return app;
    }
}