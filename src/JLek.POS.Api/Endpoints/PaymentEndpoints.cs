using JLek.POS.Api.Requests;
using JLek.POS.Application.Features.Payments.Commands.ReceivePayment;
using JLek.POS.Application.Features.Payments.Commands.RefundPayment;
using JLek.POS.Application.Features.Payments.Queries.GetPaymentById;
using JLek.POS.Application.Features.Payments.Queries.GetPaymentsByOrderId;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Payments;
using Microsoft.AspNetCore.Mvc;

namespace JLek.POS.Api.Endpoints;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/payments");

        // POST /payments
        group.MapPost("/", async (
            ReceivePaymentRequest request,
            [FromServices] ReceivePaymentCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(
                new ReceivePaymentCommand(
                    OrderId.From(request.OrderId),
                    request.AmountReceived,
                    (PaymentMethod)request.Method),
                cancellationToken);

            return Results.Created(
                $"/payments/{result.Id}",
                result);
        });

        // POST /payments/{id}/refund
        group.MapPost("/{id:guid}/refund", async (
            Guid id,
            RefundPaymentRequest request,
            [FromServices] RefundPaymentCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(
                new RefundPaymentCommand(
                    PaymentId.From(id),
                    request.Reason),
                cancellationToken);

            return Results.Ok(result);
        });

        // GET /payments/{id}
        group.MapGet("/{id:guid}", async (
            Guid id,
            [FromServices] GetPaymentByIdQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(
                new GetPaymentByIdQuery(
                    PaymentId.From(id)),
                cancellationToken);

            if (result is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        });

        // GET /payments?orderId={orderId}
        group.MapGet("/", async (
            Guid? orderId,
            [FromServices] GetPaymentsByOrderIdQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            if (orderId is null)
            {
                throw new InvalidOperationException("orderId query parameter is required.");
            }

            var result = await handler.Handle(
                new GetPaymentsByOrderIdQuery(
                    OrderId.From(orderId.Value)),
                cancellationToken);

            return Results.Ok(result);
        });

        return app;
    }
}