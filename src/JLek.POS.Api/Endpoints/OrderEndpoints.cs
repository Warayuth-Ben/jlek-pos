using JLek.POS.Api.Requests;
using JLek.POS.Api.Responses;
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

            var response = order.ToResponse();

            return Results.Created(
                $"/orders/{response.Id}",
                response);
        });

        return app;
    }
}
