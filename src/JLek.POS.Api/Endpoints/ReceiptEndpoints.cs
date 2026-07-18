using JLek.POS.Api.Requests;
using JLek.POS.Application.Features.Receipt.Commands.PrintCustomerReceipt;
using JLek.POS.Application.Features.Receipt.Commands.PrintKitchenTicket;
using JLek.POS.Application.Features.Receipt.Commands.PrintRefundReceipt;
using JLek.POS.Application.Features.Receipt.Models;
using Microsoft.AspNetCore.Mvc;

namespace JLek.POS.Api.Endpoints;

public static class ReceiptEndpoints
{
    public static IEndpointRouteBuilder MapReceiptEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/receipts");

        // POST /receipts/customer-print
        group.MapPost("/customer-print", async (
            CustomerPrintRequest request,
            [FromServices] PrintCustomerReceiptCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            if (request.OrderId == Guid.Empty)
            {
                return Results.ValidationProblem(
                    new Dictionary<string, string[]>
                    {
                        ["orderId"] = ["OrderId is required."]
                    });
            }

            var result = await handler.Handle(
                new PrintCustomerReceiptCommand(
                    request.OrderId,
                    request.IsReprint,
                    request.Copies),
                cancellationToken);

            return Results.Ok(result);
        })
        .WithName("PrintCustomerReceipt")
        .WithSummary("Print customer receipt")
        .WithDescription("Prints a customer receipt for the specified order. Supports reprint with isReprint flag.")
        .Produces<PrintResult>()
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError);

        // POST /receipts/kitchen-print
        group.MapPost("/kitchen-print", async (
            KitchenPrintRequest request,
            [FromServices] PrintKitchenTicketCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            if (request.TicketNumber <= 0)
            {
                return Results.ValidationProblem(
                    new Dictionary<string, string[]>
                    {
                        ["ticketNumber"] = ["TicketNumber must be greater than 0."]
                    });
            }

            var result = await handler.Handle(
                new PrintKitchenTicketCommand(
                    request.TicketNumber,
                    request.Copies),
                cancellationToken);

            return Results.Ok(result);
        })
        .WithName("PrintKitchenTicket")
        .WithSummary("Print kitchen ticket")
        .WithDescription("Prints a kitchen ticket for the specified ticket number.")
        .Produces<PrintResult>()
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError);

        // POST /receipts/refund-print
        group.MapPost("/refund-print", async (
            RefundPrintRequest request,
            [FromServices] PrintRefundReceiptCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            if (request.PaymentId == Guid.Empty)
            {
                return Results.ValidationProblem(
                    new Dictionary<string, string[]>
                    {
                        ["paymentId"] = ["PaymentId is required."]
                    });
            }

            var result = await handler.Handle(
                new PrintRefundReceiptCommand(
                    request.PaymentId,
                    request.IsReprint,
                    request.Copies),
                cancellationToken);

            return Results.Ok(result);
        })
        .WithName("PrintRefundReceipt")
        .WithSummary("Print refund receipt")
        .WithDescription("Prints a refund receipt for the specified payment. Supports reprint with isReprint flag.")
        .Produces<PrintResult>()
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}