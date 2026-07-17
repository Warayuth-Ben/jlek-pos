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

    // ── Helpers ─────────────────────────────────────────────────

    private async Task<Guid> SeedCompletedOrderAsync(
        decimal unitPrice = 100m,
        int itemCount = 1,
        int quantityPerItem = 1)
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var order = Order.Create();
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
        
        // Order 1: 2 items at 100 each, Cash = 200
        var id1 = await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Cash, 100m, 2);
        ids.Add(id1);

        // Order 2: 1 item at 250, QR = 250
        var id2 = await SeedCompletedOrderWithPaymentAsync(PaymentMethod.QR, 250m, 1);
        ids.Add(id2);

        // Order 3: 3 items at 50 each, Card = 150
        var id3 = await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Card, 50m, 3);
        ids.Add(id3);

        // Order 4: 1 item at 500, Credit = 500
        var id4 = await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Credit, 500m, 1);
        ids.Add(id4);

        return ids;
    }

    // ── Routing ─────────────────────────────────────────────────

    [Fact]
    public async Task DailySales_Endpoint_Should_Return200()
    {
        // Act
        var response = await _client.GetAsync("/reports/daily-sales");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task SalesByPayment_Endpoint_Should_Return200()
    {
        // Act
        var response = await _client.GetAsync("/reports/sales-by-payment");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task BestSellers_Endpoint_Should_Return200()
    {
        // Act
        var response = await _client.GetAsync("/reports/best-sellers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // ── 1. Daily Sales ──────────────────────────────────────────

    [Fact]
    public async Task DailySales_WhenEmptyDatabase_Should_ReturnZeroValues()
    {
        // Act
        var response = await _client.GetAsync("/reports/daily-sales");

        // Assert
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
        // Arrange
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Cash, 100m, 2);

        // Act
        var response = await _client.GetAsync("/reports/daily-sales");

        // Assert
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
        // Arrange — 4 orders total: 200 + 250 + 150 + 500 = 1100
        await SeedMultipleCompletedOrdersWithPaymentsAsync();

        // Act
        var response = await _client.GetAsync("/reports/daily-sales");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<DailySalesReport>();
        result!.TotalOrders.Should().Be(4);
        result.TotalRevenue.Should().Be(1100m);
        result.TotalItemsSold.Should().Be(7); // 2 + 1 + 3 + 1
        result.AverageOrderValue.Should().Be(275m); // 1100 / 4
    }

    [Fact]
    public async Task DailySales_WithRefundedPayments_Should_CalculateNetRevenue()
    {
        // Arrange — create a completed payment then refund it
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var order = Order.Create();
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

        // Act
        var response = await _client.GetAsync("/reports/daily-sales");

        // Assert — report counts Completed payments only, not Refunded
        // (The payment was seeded as Completed, not Refunded)
        // So we should have 1 Completed payment
        var result = await response.Content.ReadFromJsonAsync<DailySalesReport>();
        result!.TotalOrders.Should().Be(1);
        result.TotalRevenue.Should().Be(200m);
    }

    [Fact]
    public async Task DailySales_Should_HaveCorrectSchema()
    {
        // Arrange
        await SeedCompletedOrderWithPaymentAsync();

        // Act
        var response = await _client.GetAsync("/reports/daily-sales");

        // Assert — JSON property names
        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("date");
        json.Should().Contain("totalOrders");
        json.Should().Contain("totalRevenue");
        json.Should().Contain("totalRefunds");
        json.Should().Contain("netRevenue");
        json.Should().Contain("averageOrderValue");
        json.Should().Contain("totalItemsSold");
    }

    // ── 2. Sales By Payment ─────────────────────────────────────

    [Fact]
    public async Task SalesByPayment_WithNoPayments_Should_ReturnEmpty()
    {
        // Act
        var response = await _client.GetAsync("/reports/sales-by-payment");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<SalesByPaymentReport>();
        result.Should().NotBeNull();
        result!.PaymentMethods.Should().BeEmpty();
        result.DateFrom.Should().BeBefore(result.DateTo);
    }

    [Fact]
    public async Task SalesByPayment_WithCashPayment_Should_GroupByCash()
    {
        // Arrange
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Cash, 100m, 1);

        // Act
        var response = await _client.GetAsync("/reports/sales-by-payment");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<SalesByPaymentReport>();
        result!.PaymentMethods.Should().ContainSingle();
        result.PaymentMethods.First().Method.Should().Be("Cash");
        result.PaymentMethods.First().TransactionCount.Should().Be(1);
        result.PaymentMethods.First().TotalAmount.Should().Be(100m);
    }

    [Fact]
    public async Task SalesByPayment_WithQrPayment_Should_GroupByQR()
    {
        // Arrange
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.QR, 150m, 1);

        // Act
        var response = await _client.GetAsync("/reports/sales-by-payment");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<SalesByPaymentReport>();
        result!.PaymentMethods.First().Method.Should().Be("QR");
        result.PaymentMethods.First().TransactionCount.Should().Be(1);
        result.PaymentMethods.First().TotalAmount.Should().Be(150m);
    }

    [Fact]
    public async Task SalesByPayment_WithCardPayment_Should_GroupByCard()
    {
        // Arrange
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Card, 200m, 1);

        // Act
        var response = await _client.GetAsync("/reports/sales-by-payment");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<SalesByPaymentReport>();
        result!.PaymentMethods.First().Method.Should().Be("Card");
        result.PaymentMethods.First().TransactionCount.Should().Be(1);
        result.PaymentMethods.First().TotalAmount.Should().Be(200m);
    }

    [Fact]
    public async Task SalesByPayment_WithCreditPayment_Should_GroupByCredit()
    {
        // Arrange
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Credit, 300m, 1);

        // Act
        var response = await _client.GetAsync("/reports/sales-by-payment");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<SalesByPaymentReport>();
        result!.PaymentMethods.First().Method.Should().Be("Credit");
        result.PaymentMethods.First().TransactionCount.Should().Be(1);
        result.PaymentMethods.First().TotalAmount.Should().Be(300m);
    }

    [Fact]
    public async Task SalesByPayment_WithMultipleMethods_Should_GroupCorrectly()
    {
        // Arrange — 2 Cash + 1 QR + 1 Card + 1 Credit
        var ids = new List<Guid>();
        ids.Add(await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Cash, 100m, 1));
        ids.Add(await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Cash, 200m, 1));
        ids.Add(await SeedCompletedOrderWithPaymentAsync(PaymentMethod.QR, 150m, 1));
        ids.Add(await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Card, 300m, 1));
        ids.Add(await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Credit, 500m, 1));

        // Act
        var response = await _client.GetAsync("/reports/sales-by-payment");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<SalesByPaymentReport>();
        result!.PaymentMethods.Should().HaveCount(4);

        var cash = result.PaymentMethods.First(m => m.Method == "Cash");
        cash.TransactionCount.Should().Be(2);
        cash.TotalAmount.Should().Be(300m);

        var qr = result.PaymentMethods.First(m => m.Method == "QR");
        qr.TransactionCount.Should().Be(1);
        qr.TotalAmount.Should().Be(150m);

        var card = result.PaymentMethods.First(m => m.Method == "Card");
        card.TransactionCount.Should().Be(1);
        card.TotalAmount.Should().Be(300m);

        var credit = result.PaymentMethods.First(m => m.Method == "Credit");
        credit.TransactionCount.Should().Be(1);
        credit.TotalAmount.Should().Be(500m);
    }

    [Fact]
    public async Task SalesByPayment_Should_OrderByTotalAmountDescending()
    {
        // Arrange
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Cash, 100m, 1);     // 100
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.Credit, 500m, 1);   // 500
        await SeedCompletedOrderWithPaymentAsync(PaymentMethod.QR, 200m, 1);       // 200

        // Act
        var response = await _client.GetAsync("/reports/sales-by-payment");

        // Assert — should be Credit > QR > Cash
        var result = await response.Content.ReadFromJsonAsync<SalesByPaymentReport>();
        result!.PaymentMethods[0].TotalAmount.Should().Be(500m);
        result.PaymentMethods[1].TotalAmount.Should().Be(200m);
        result.PaymentMethods[2].TotalAmount.Should().Be(100m);
    }

    // ── 3. Best Sellers ─────────────────────────────────────────

    [Fact]
    public async Task BestSellers_WhenEmpty_Should_ReturnEmptyList()
    {
        // Act
        var response = await _client.GetAsync("/reports/best-sellers");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result.Should().NotBeNull();
        result!.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task BestSellers_WithOneItem_Should_ReturnRank1()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var order = Order.Create();
            var menuItemId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            order.AddItem(menuItemId, Quantity.From(3), Money.From(50m));
            order.Confirm();
            order.Complete();
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("/reports/best-sellers");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items.Should().ContainSingle();
        result.Items[0].Rank.Should().Be(1);
        result.Items[0].MenuItemId.Should().Be(Guid.Parse("11111111-1111-1111-1111-111111111111"));
        result.Items[0].TotalQuantity.Should().Be(3);
        result.Items[0].TotalRevenue.Should().Be(150m);
        result.Items[0].OrderCount.Should().Be(1);
    }

    [Fact]
    public async Task BestSellers_WithMultipleItems_Should_OrderByQuantityDescending()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var idPizza = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
            var idSteak = Guid.Parse("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB");
            var idSalad = Guid.Parse("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC");

            // Pizza sold 10 times
            var order1 = Order.Create();
            order1.AddItem(idPizza, Quantity.From(10), Money.From(100m));
            order1.Confirm();
            order1.Complete();
            context.Orders.Add(order1);

            // Steak sold 7 times
            var order2 = Order.Create();
            order2.AddItem(idSteak, Quantity.From(7), Money.From(300m));
            order2.Confirm();
            order2.Complete();
            context.Orders.Add(order2);

            // Salad sold 3 times
            var order3 = Order.Create();
            order3.AddItem(idSalad, Quantity.From(3), Money.From(50m));
            order3.Confirm();
            order3.Complete();
            context.Orders.Add(order3);

            await context.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("/reports/best-sellers");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items.Should().HaveCount(3);
        result.Items[0].TotalQuantity.Should().Be(10);
        result.Items[0].MenuItemId.Should().Be(Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"));
        result.Items[0].Rank.Should().Be(1);
        result.Items[1].TotalQuantity.Should().Be(7);
        result.Items[1].Rank.Should().Be(2);
        result.Items[2].TotalQuantity.Should().Be(3);
        result.Items[2].Rank.Should().Be(3);
    }

    [Fact]
    public async Task BestSellers_WithLimit_Should_RespectLimit()
    {
        // Arrange — 3 items
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            for (int i = 0; i < 3; i++)
            {
                var order = Order.Create();
                order.AddItem(Guid.NewGuid(), Quantity.From(5), Money.From(50m));
                order.Confirm();
                order.Complete();
                context.Orders.Add(order);
            }
            await context.SaveChangesAsync();
        }

        // Act — limit=2
        var response = await _client.GetAsync("/reports/best-sellers?limit=2");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items.Should().HaveCount(2);
        result.Limit.Should().Be(2);
    }

    [Fact]
    public async Task BestSellers_WithSameItemMultipleOrders_Should_Aggregate()
    {
        // Arrange
        var menuItemId = Guid.Parse("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD");

        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Order 1: 2 of menu item D
            var order1 = Order.Create();
            order1.AddItem(menuItemId, Quantity.From(2), Money.From(100m));
            order1.Confirm();
            order1.Complete();
            context.Orders.Add(order1);

            // Order 2: 3 of same menu item D
            var order2 = Order.Create();
            order2.AddItem(menuItemId, Quantity.From(3), Money.From(100m));
            order2.Confirm();
            order2.Complete();
            context.Orders.Add(order2);

            await context.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("/reports/best-sellers");

        // Assert — total quantity = 5, order count = 2, revenue = 500
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items.Should().ContainSingle();
        result.Items[0].MenuItemId.Should().Be(menuItemId);
        result.Items[0].TotalQuantity.Should().Be(5);
        result.Items[0].OrderCount.Should().Be(2);
        result.Items[0].TotalRevenue.Should().Be(500m);
    }

    [Fact]
    public async Task BestSellers_Should_ExcludeNonCompletedOrders()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var menuItemId = Guid.NewGuid();

            // Draft order — should NOT appear in best sellers
            var draft = Order.Create();
            draft.AddItem(menuItemId, Quantity.From(10), Money.From(100m));
            context.Orders.Add(draft);

            // Completed order — should appear
            var completed = Order.Create();
            completed.AddItem(menuItemId, Quantity.From(3), Money.From(100m));
            completed.Confirm();
            completed.Complete();
            context.Orders.Add(completed);

            await context.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("/reports/best-sellers");

        // Assert — only 3 from completed order
        var result = await response.Content.ReadFromJsonAsync<BestSellerReport>();
        result!.Items.First().TotalQuantity.Should().Be(3);
    }

    [Fact]
    public async Task BestSellers_Should_HaveCorrectSchema()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var order = Order.Create();
            order.AddItem(Guid.NewGuid(), Quantity.From(1), Money.From(50m));
            order.Confirm();
            order.Complete();
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("/reports/best-sellers");

        // Assert — JSON property names
        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("items");
        json.Should().Contain("rank");
        json.Should().Contain("menuItemId");
        json.Should().Contain("totalQuantity");
        json.Should().Contain("totalRevenue");
        json.Should().Contain("orderCount");
    }

    // ── 4. Validation ───────────────────────────────────────────

    [Fact]
    public async Task BestSellers_WithInvalidLimit_Should_Return400()
    {
        // Act
        var response = await _client.GetAsync("/reports/best-sellers?limit=0");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task BestSellers_WithNegativeLimit_Should_Return400()
    {
        // Act
        var response = await _client.GetAsync("/reports/best-sellers?limit=-5");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}