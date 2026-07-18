using JLek.POS.Application.Features.Reports.Queries.BestSellers;
using JLek.POS.Application.Features.Reports.Queries.DailySales;
using JLek.POS.Application.Features.Reports.Queries.SalesByPayment;
using JLek.POS.Application.Features.Reports.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JLek.POS.Api.Endpoints;

public static class ReportingEndpoints
{
    public static IEndpointRouteBuilder MapReportingEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/reports");

        // GET /reports/daily-sales
        group.MapGet("/daily-sales", async (
            DateOnly? date,
            [FromServices] GetDailySalesReportQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(
                new GetDailySalesReportQuery(date),
                cancellationToken);

            return Results.Ok(result);
        })
        .WithName("GetDailySalesReport")
        .WithSummary("Get daily sales report")
        .WithDescription("Returns total revenue, order count, average order value, items sold, and refunds for a specific date. Defaults to today.")
        .Produces<DailySalesReport>()
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError);

        // GET /reports/sales-by-payment
        group.MapGet("/sales-by-payment", async (
            DateOnly? dateFrom,
            DateOnly? dateTo,
            [FromServices] GetSalesByPaymentReportQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(
                new GetSalesByPaymentReportQuery(dateFrom, dateTo),
                cancellationToken);

            return Results.Ok(result);
        })
        .WithName("GetSalesByPaymentReport")
        .WithSummary("Get sales by payment method")
        .WithDescription("Returns sales grouped by payment method (Cash, Card, QR, Credit) for a date range. Defaults to last 7 days.")
        .Produces<SalesByPaymentReport>()
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError);

        // GET /reports/best-sellers
        group.MapGet("/best-sellers", async (
            DateOnly? dateFrom,
            DateOnly? dateTo,
            int? limit,
            [FromServices] GetBestSellerReportQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            if (limit.HasValue && limit.Value <= 0)
            {
                return Results.ValidationProblem(
                    new Dictionary<string, string[]>
                    {
                        ["limit"] = ["limit must be greater than 0."]
                    });
            }

            if (dateFrom.HasValue && dateTo.HasValue && dateFrom.Value > dateTo.Value)
            {
                return Results.ValidationProblem(
                    new Dictionary<string, string[]>
                    {
                        ["dateFrom"] = ["dateFrom must be less than or equal to dateTo."]
                    });
            }

            var result = await handler.Handle(
                new GetBestSellerReportQuery(dateFrom, dateTo, limit ?? 10),
                cancellationToken);

            return Results.Ok(result);
        })
        .WithName("GetBestSellerReport")
        .WithSummary("Get best selling menu items")
        .WithDescription("Returns top selling menu items ranked by quantity sold for a date range. Defaults to last 30 days, top 10.")
        .Produces<BestSellerReport>()
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}