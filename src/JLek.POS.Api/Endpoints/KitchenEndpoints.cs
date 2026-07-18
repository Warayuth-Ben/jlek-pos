using JLek.POS.Application.Features.Kitchen.Commands.AddKitchenItem;
using JLek.POS.Application.Features.Kitchen.Commands.CompletePreparation;
using JLek.POS.Application.Features.Kitchen.Commands.CreateKitchenTicket;
using JLek.POS.Application.Features.Kitchen.Commands.ServeKitchenTicket;
using JLek.POS.Application.Features.Kitchen.Commands.StartPreparation;
using JLek.POS.Application.Features.Kitchen.Queries.GetActiveKitchenTickets;
using JLek.POS.Application.Features.Kitchen.Queries.GetKitchenTicketById;
using JLek.POS.Application.Features.Kitchen.Queries.GetKitchenTickets;
using JLek.POS.Domain.Kitchen;
using Microsoft.AspNetCore.Mvc;

namespace JLek.POS.Api.Endpoints;

public static class KitchenEndpoints
{
    public static IEndpointRouteBuilder MapKitchenEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/kitchen");

        // POST /kitchen
        group.MapPost("/", async (
            int ticketNumber,
            string itemName,
            int quantity,
            string? notes,
            [FromServices] CreateKitchenTicketCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new CreateKitchenTicketCommand(
                    ticketNumber, itemName, quantity, notes),
                cancellationToken);

            return Results.Created(
                $"/kitchen/{response.Id}",
                response);
        });

        // GET /kitchen
        group.MapGet("/", async (
            [FromServices] GetKitchenTicketsQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var tickets = await handler.Handle(
                new GetKitchenTicketsQuery(),
                cancellationToken);

            return Results.Ok(tickets);
        });

        // GET /kitchen/active
        group.MapGet("/active", async (
            [FromServices] GetActiveKitchenTicketsQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var tickets = await handler.Handle(
                new GetActiveKitchenTicketsQuery(),
                cancellationToken);

            return Results.Ok(tickets);
        });

        // GET /kitchen/{id}
        group.MapGet("/{id:guid}", async (
            Guid id,
            [FromServices] GetKitchenTicketByIdQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new GetKitchenTicketByIdQuery(
                    KitchenTicketId.From(id)),
                cancellationToken);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        });

        // POST /kitchen/{id}/items
        group.MapPost("/{id:guid}/items", async (
            Guid id,
            string itemName,
            int quantity,
            string? notes,
            [FromServices] AddKitchenItemCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new AddKitchenItemCommand(
                    KitchenTicketId.From(id),
                    itemName,
                    quantity,
                    notes),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /kitchen/{id}/start
        group.MapPost("/{id:guid}/start", async (
            Guid id,
            [FromServices] StartPreparationCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new StartPreparationCommand(
                    KitchenTicketId.From(id)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /kitchen/{id}/complete
        group.MapPost("/{id:guid}/complete", async (
            Guid id,
            [FromServices] CompletePreparationCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new CompletePreparationCommand(
                    KitchenTicketId.From(id)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /kitchen/{id}/serve
        group.MapPost("/{id:guid}/serve", async (
            Guid id,
            [FromServices] ServeKitchenTicketCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new ServeKitchenTicketCommand(
                    KitchenTicketId.From(id)),
                cancellationToken);

            return Results.Ok(response);
        });

        return app;
    }
}