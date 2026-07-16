using JLek.POS.Application.Features.Tables.Commands.AssignTable;
using JLek.POS.Application.Features.Tables.Commands.CreateDiningTable;
using JLek.POS.Application.Features.Tables.Commands.MergeTables;
using JLek.POS.Application.Features.Tables.Commands.ReleaseTable;
using JLek.POS.Application.Features.Tables.Commands.SplitTable;
using JLek.POS.Application.Features.Tables.Commands.TransferTable;
using JLek.POS.Application.Features.Tables.Queries.GetAvailableDiningTables;
using JLek.POS.Application.Features.Tables.Queries.GetDiningTableById;
using JLek.POS.Application.Features.Tables.Queries.GetDiningTables;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace JLek.POS.Api.Endpoints;

public static class TableEndpoints
{
    public static IEndpointRouteBuilder MapTableEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tables");

        // POST /tables
        group.MapPost("/", async (
            string name,
            [FromServices] CreateDiningTableCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new CreateDiningTableCommand(name),
                cancellationToken);

            return Results.Created(
                $"/tables/{response.Id}",
                response);
        });

        // GET /tables
        group.MapGet("/", async (
            [FromServices] GetDiningTablesQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var tables = await handler.Handle(
                new GetDiningTablesQuery(),
                cancellationToken);

            return Results.Ok(tables);
        });

        // GET /tables/available
        group.MapGet("/available", async (
            [FromServices] GetAvailableDiningTablesQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var tables = await handler.Handle(
                new GetAvailableDiningTablesQuery(),
                cancellationToken);

            return Results.Ok(tables);
        });

        // GET /tables/{id}
        group.MapGet("/{id:guid}", async (
            Guid id,
            [FromServices] GetDiningTableByIdQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new GetDiningTableByIdQuery(
                    TableId.From(id)),
                cancellationToken);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        });

        // POST /tables/{id}/assign
        group.MapPost("/{id:guid}/assign", async (
            Guid id,
            Guid sessionId,
            [FromServices] AssignTableCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new AssignTableCommand(
                    TableId.From(id),
                    new OrderSessionId(sessionId)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /tables/{id}/transfer
        group.MapPost("/{id:guid}/transfer", async (
            Guid id,
            Guid destinationTableId,
            [FromServices] TransferTableCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new TransferTableCommand(
                    TableId.From(id),
                    TableId.From(destinationTableId)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /tables/{id}/merge
        group.MapPost("/{id:guid}/merge", async (
            Guid id,
            Guid tableToMergeId,
            [FromServices] MergeTablesCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new MergeTablesCommand(
                    TableId.From(id),
                    TableId.From(tableToMergeId)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /tables/{id}/split
        group.MapPost("/{id:guid}/split", async (
            Guid id,
            Guid tableToSplitId,
            [FromServices] SplitTableCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new SplitTableCommand(
                    TableId.From(id),
                    TableId.From(tableToSplitId)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /tables/{id}/release
        group.MapPost("/{id:guid}/release", async (
            Guid id,
            [FromServices] ReleaseTableCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new ReleaseTableCommand(
                    TableId.From(id)),
                cancellationToken);

            return Results.Ok(response);
        });

        return app;
    }
}