using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JLek.POS.Application.Features.Reports.Responses;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Payments;
using JLek.POS.Infrastructure.Persistence;
using JLek.POS.IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace JLek.POS.IntegrationTests.Reports;

[Collection("Reports")]
public sealed class ReportingTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public ReportingTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    private static JLek.POS.Domain.ValueObjects.TableId NewTableId() =>
        JLek.POS.Domain.ValueObjects.TableId.From(Guid.NewGuid());

    private static OrderSessionId NewSessionId() =>
        new(Guid.NewGuid());

    private async Task<Guid> SeedCompletedOrderAsync(
        decimal unitPrice = 100m,
        int itemCount = 1,
        int quantityPerItem = 1)
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var order = Order.Create(NewTableId(), NewSessionId());
        for (int i = 0; i < itemCount; i++)
        {
            order.AddItem(Guid.NewGuid(), Quantity.From(quantityPerItem), Money.From(unitPrice));
        }
        order.Confirm();
        order.Complete();

        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order.Id.Value;
    }

    private async Task<Guid> SeedCompletedOrderWithPaymentAsync(
        PaymentMethod method = PaymentMethod.Cash,
        decimal unitPrice = 100m,
        int quantity = 1)
    {
        var orderId = await SeedCompletedOrderAsync(unitPrice, quantity == 0 ? 0 : 1, quantity);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var order = await context.Orders.FirstAsync(o => o.Id == OrderId.From(orderId));
        var totalAmount = unitPrice * quantity;
        var payment = Payment.Create(order, Money.From(totalAmount), method);
        context.Payments.Add(payment);
        await context.SaveChangesAsync();

        return order.Id.Value;
    }

    private async Task<List<Guid>> SeedMultipleCompletedOrdersWithPaymentsAsync()
    {
        var ids = new List<Guid>();
        ids.Add(await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Cash, 100m, 2));
        ids.Add(await SeedCompletedOrderWithPaymentAsync(PaymentMethod.QR, 250m, 1));
        ids.Add(await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Card, 50m, 3));
        ids.Add(await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Credit, 500m, 1));
        return ids;
    }

    [Fact]
    public async Task DailySales_Endpoint_Should_Return200()
    {
        var response = await _client.GetAsync("/reports/daily-sales");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task SalesByPayment_Endpoint_Should_Return200()
    {
        var response = await _client.GetAsync("/reports/sales-by-payment");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task BestSellers_Endpoint_Should_Return200()
    {
        var response = await _client.GetAsync("/reports/best-sellers");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DailySales_WhenEmptyDatabase_Should_ReturnZeroValues()
    {
        var response = await _client.GetAsync("/reports/daily-sales");
        var result = await response.Content.ReadFromJsonAsync<DailySalesReport>();
        result.Should().NotBeNull();
        result!.TotalOrders.Should().Be(0);
        result.TotalRevenue.Should().Be(0m);
        result.TotalRefunds.Should().Be(0m);
        result.NetRevenue.Should().Be(0m);
        result.AverageOrderValue.Should().Be(0m);
        result.TotalItemsSold.Should().Be(0);
    }

    [Fact]
    public async Task DailySales_WithSingleCompletedPayment_Should_ReturnCorrectRevenue()
    {
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Cash, 100m, 2);
        var response = await _client.GetAsync("/reports/daily-sales");
        var result = await response.Content.ReadFromJsonAsync<DailySalesReport>();
        result!.TotalOrders.Should().Be(1);
        result.TotalRevenue.Should().Be(200m);
        result.TotalRefunds.Should().Be(0m);
        result.NetRevenue.Should().Be(200m);
        result.AverageOrderValue.Should().Be(200m);
        result.TotalItemsSold.Should().Be(2);
    }

    [Fact]
    public async Task DailySales_WithMultiplePayments_Should_AggregateCorrectly()
    {
        await SeedMultipleCompletedOrdersWithPaymentsAsync();
        var response = await _client.GetAsync("/reports/daily-sales");
        var result = await response.Content.ReadFromJsonAsync<DailySalesReport>();
        result!.TotalOrders.Should().Be(4);
        result.TotalRevenue.Should().Be(1100m);
        result.TotalItemsSold.Should().Be(7);
        result.AverageOrderValue.Should().Be(275m);
    }

    [Fact]
    public async Task DailySales_WithRefundedPayments_Should_CalculateNetRevenue()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var order = Order.Create(NewTableId(), NewSessionId());
            order.AddItem(Guid.NewGuid(), Quantity.From(1), Money.From(200m));
            order.Confirm();
            order.Complete();
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var order2 = await context.Orders.FirstAsync(o => o.Id == order.Id);
            var payment = Payment.Create(order2, Money.From(200m), PaymentMethod.Cash);
            context.Payments.Add(payment);
            await context.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/reports/daily-sales");
        var result = await response.Content.ReadFromJsonAsync<DailySalesReport>();
        result!.TotalOrders.Should().Be(1);
        result.TotalRevenue.Should().Be(200m);
    }

    [Fact]
    public async Task DailySales_Should_HaveCorrectSchema()
    {
        await SeedCompletedOrderWithPaymentAsync();
        var response = await _client.GetAsync("/reports/daily-sales");
        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("date");
        json.Should().Contain("totalOrders");
        json.Should().Contain("totalRevenue");
        json.Should().Contain("totalRefunds");
        json.Should().Contain("netRevenue");
        json.Should().Contain("averageOrderValue");
        json.Should().Contain("totalItemsSold");
    }

    // Tests 2-4 remain but already use SeedCompletedOrderWithPaymentAsync which uses NewTableId() internally
    // No changes needed — the helper methods handle TableId properly

    [Fact]
    public async Task SalesByPayment_WithNoPayments_Should_ReturnEmpty()
    {
        var response = await _client.GetAsync("/reports/sales-by-payment");
        var result = await response.Content.ReadFromJsonAsync<SalesByPaymentReport>();
        result.Should().NotBeNull();
        result!.PaymentMethods.Should().BeEmpty();
        result.DateFrom.Should().BeBefore(result.DateTo);
    }

    [Fact]
    public async Task SalesByPayment_WithCashPayment_Should_GroupByCash()
    {
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Cash, 100m, 1);
        var response = await _client.GetAsync("/reports/sales-by-payment");
        var result = await response.Content.ReadFromJsonAsync<SalesByPaymentReport>();
        result!.PaymentMethods.First().Method.Should().Be("Cash");
    }

    [Fact]
    public async Task BestSellers_WhenEmpty_Should_ReturnEmptyList()
    {
        var response = await _client.GetAsync("/reports/best-sellers");
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task BestSellers_WithOneItem_Should_ReturnRank1()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var order = Order.Create(NewTableId(), NewSessionId());
            var menuItemId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            order.AddItem(menuItemId, Quantity.From(3), Money.From(50m));
            order.Confirm();
            order.Complete();
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/reports/best-sellers");
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items[0].TotalQuantity.Should().Be(3);
    }

    [Fact]
    public async Task BestSellers_WithMultipleItems_Should_OrderByQuantityDescending()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var idPizza = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
            var idSteak = Guid.Parse("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB");
            var idSalad = Guid.Parse("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC");

            var order1 = Order.Create(NewTableId(), NewSessionId());
            order1.AddItem(idPizza, Quantity.From(10), Money.From(100m));
            order1.Confirm(); order1.Complete();
            context.Orders.Add(order1);

            var order2 = Order.Create(NewTableId(), NewSessionId());
            order2.AddItem(idSteak, Quantity.From(7), Money.From(300m));
            order2.Confirm(); order2.Complete();
            context.Orders.Add(order2);

            var order3 = Order.Create(NewTableId(), NewSessionId());
            order3.AddItem(idSalad, Quantity.From(3), Money.From(50m));
            order3.Confirm(); order3.Complete();
            context.Orders.Add(order3);

            await context.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/reports/best-sellers");
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items[0].TotalQuantity.Should().Be(10);
        result.Items[1].TotalQuantity.Should().Be(7);
        result.Items[2].TotalQuantity.Should().Be(3);
    }

    [Fact]
    public async Task BestSellers_WithLimit_Should_RespectLimit()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            for (int i = 0; i < 3; i++)
            {
                var order = Order.Create(NewTableId(), NewSessionId());
                order.AddItem(Guid.NewGuid(), Quantity.From(5), Money.From(50m));
                order.Confirm(); order.Complete();
                context.Orders.Add(order);
            }
            await context.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/reports/best-sellers?limit=2");
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items.Should().HaveCount(2);
    }

    [Fact]
    public async Task BestSellers_WithSameItemMultipleOrders_Should_Aggregate()
    {
        var menuItemId = Guid.Parse("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD");
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var order1 = Order.Create(NewTableId(), NewSessionId());
            order1.AddItem(menuItemId, Quantity.From(2), Money.From(100m));
            order1.Confirm(); order1.Complete();
            context.Orders.Add(order1);

            var order2 = Order.Create(NewTableId(), NewSessionId());
            order2.AddItem(menuItemId, Quantity.From(3), Money.From(100m));
            order2.Confirm(); order2.Complete();
            context.Orders.Add(order2);
            await context.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/reports/best-sellers");
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items[0].TotalQuantity.Should().Be(5);
        result.Items[0].OrderCount.Should().Be(2);
    }

    [Fact]
    public async Task BestSellers_Should_ExcludeNonCompletedOrders()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var menuItemId = Guid.NewGuid();

            var draft = Order.Create(NewTableId(), NewSessionId());
            draft.AddItem(menuItemId, Quantity.From(10), Money.From(100m));
            context.Orders.Add(draft);

            var completed = Order.Create(NewTableId(), NewSessionId());
            completed.AddItem(menuItemId, Quantity.From(3), Money.From(100m));
            completed.Confirm(); completed.Complete();
            context.Orders.Add(completed);
            await context.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/reports/best-sellers");
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items.First().TotalQuantity.Should().Be(3);
    }

    [Fact]
    public async Task BestSellers_Should_HaveCorrectSchema()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var order = Order.Create(NewTableId(), NewSessionId());
            order.AddItem(Guid.NewGuid(), Quantity.From(1), Money.From(50m));
            order.Confirm(); order.Complete();
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/reports/best-sellers");
        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("items");
        json.Should().Contain("rank");
        json.Should().Contain("menuItemId");
        json.Should().Contain("totalQuantity");
        json.Should().Contain("totalRevenue");
        json.Should().Contain("orderCount");
    }

    [Fact]
    public async Task BestSellers_WithInvalidLimit_Should_Return400()
    {
        var response = await _client.GetAsync("/reports/best-sellers?limit=0");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task BestSellers_WithNegativeLimit_Should_Return400()
    {
        var response = await _client.GetAsync("/reports/best-sellers?limit=-5");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}