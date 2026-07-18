using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JLek.POS.Api.Requests;
using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Features.Receipt.Configuration;
using JLek.POS.Application.Features.Receipt.DTOs;
using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Application.Features.Receipt.Services;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Kitchen;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Payments;
using JLek.POS.Infrastructure.Persistence;
using JLek.POS.Infrastructure.Printing;
using JLek.POS.IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace JLek.POS.IntegrationTests.Receipts;

[Collection("Receipts")]
public sealed class ReceiptTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public ReceiptTests(CustomWebApplicationFactory factory)
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

    private async Task<Guid> SeedCompletedOrderWithPaymentAsync(decimal total = 100m)
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var order = Order.Create(JLek.POS.Domain.ValueObjects.TableId.From(Guid.NewGuid()), new OrderSessionId(Guid.NewGuid()));
        order.AddItem(Guid.NewGuid(), Quantity.From(1), Money.From(total));
        order.Confirm();
        order.Complete();
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        var order2 = await context.Orders.FirstAsync(o => o.Id == order.Id);
        var payment = Payment.Create(order2, Money.From(total), PaymentMethod.Cash);
        context.Payments.Add(payment);
        await context.SaveChangesAsync();
        return order.Id.Value;
    }

    private async Task<int> SeedKitchenTicketAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var ticket = KitchenTicket.Create(1, "ผัดไทย", 2, "ไม่ใส่ถั่ว");
        context.KitchenTickets.Add(ticket);
        await context.SaveChangesAsync();
        return ticket.TicketNumber;
    }

    // ── 1. POST /receipts/customer-print ────────────────────────

    [Fact]
    public async Task CustomerPrint_WhenOrderExists_Should_ReturnOk()
    {
        // Arrange
        var orderId = await SeedCompletedOrderWithPaymentAsync(200m);
        var request = new CustomerPrintRequest(orderId);

        // Act
        var response = await _client.PostAsJsonAsync("/receipts/customer-print", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PrintResult>();
        result.Should().NotBeNull();
        result!.Success.Should().BeTrue();
        result.Status.Should().Be("Completed");
        result.PrinterName.Should().Contain("Null Printer");
    }

    [Fact]
    public async Task CustomerPrint_WithEmptyOrderId_Should_Return400()
    {
        // Arrange
        var request = new CustomerPrintRequest(Guid.Empty);

        // Act
        var response = await _client.PostAsJsonAsync("/receipts/customer-print", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CustomerPrint_WithReprint_Should_ReturnOk()
    {
        // Arrange
        var orderId = await SeedCompletedOrderWithPaymentAsync();
        var request = new CustomerPrintRequest(orderId, IsReprint: true);

        // Act
        var response = await _client.PostAsJsonAsync("/receipts/customer-print", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    // ── 2. POST /receipts/kitchen-print ─────────────────────────

    [Fact]
    public async Task KitchenPrint_WhenTicketExists_Should_ReturnOk()
    {
        // Arrange
        var ticketNumber = await SeedKitchenTicketAsync();
        var request = new KitchenPrintRequest(ticketNumber);

        // Act
        var response = await _client.PostAsJsonAsync("/receipts/kitchen-print", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PrintResult>();
        result.Should().NotBeNull();
        result!.Success.Should().BeTrue();
    }

    [Fact]
    public async Task KitchenPrint_WithInvalidTicketNumber_Should_Return400()
    {
        // Arrange
        var request = new KitchenPrintRequest(0);

        // Act
        var response = await _client.PostAsJsonAsync("/receipts/kitchen-print", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task KitchenPrint_WhenTicketNotFound_Should_ReturnOkWithFailedStatus()
    {
        // Arrange
        var request = new KitchenPrintRequest(99999);

        // Act
        var response = await _client.PostAsJsonAsync("/receipts/kitchen-print", request);

        // Assert — Handler returns failed PrintResult (not 404)
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PrintResult>();
        result!.Success.Should().BeFalse();
        result.Status.Should().Be("Failed");
        result.ErrorMessage.Should().NotBeNull();
    }

    // ── 3. POST /receipts/refund-print ──────────────────────────

    [Fact]
    public async Task RefundPrint_WhenPaymentExists_Should_ReturnOk()
    {
        // Arrange
        var orderId = await SeedCompletedOrderWithPaymentAsync();
        using var scope = _factory.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var payment = await ctx.Payments.FirstAsync(p => p.OrderId.Value == orderId);
        var request = new RefundPrintRequest(payment.Id.Value);

        // Act
        var response = await _client.PostAsJsonAsync("/receipts/refund-print", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PrintResult>();
        result.Should().NotBeNull();
        result!.Success.Should().BeTrue();
    }

    [Fact]
    public async Task RefundPrint_WithEmptyPaymentId_Should_Return400()
    {
        // Arrange
        var request = new RefundPrintRequest(Guid.Empty);

        // Act
        var response = await _client.PostAsJsonAsync("/receipts/refund-print", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    // ── 4. ReceiptFormatter ──────────────────────────────────────

    [Fact]
    public void Formatter_FormatCustomerReceipt_Should_IncludeHeaderAndItems()
    {
        // Arrange
        var config = new ReceiptConfiguration { ShopName = "Test Shop", PaperWidth = 48 };
        var formatter = new ReceiptFormatter(config);
        var data = new CustomerReceiptData
        {
            ReceiptNumber = "RC001",
            PrintedAt = new DateTime(2026, 7, 17, 12, 0, 0, DateTimeKind.Utc),
            Items = new List<ReceiptItemData>
            {
                new() { Name = "ผัดไทย", Quantity = 2, UnitPrice = 50m, Total = 100m }
            },
            Total = 100m,
            AmountReceived = 100m,
            Change = 0m,
            PaymentMethod = "Cash"
        };

        // Act
        var doc = formatter.FormatCustomerReceipt(data);

        // Assert
        doc.Title.Should().Be("ใบเสร็จรับเงิน");
        doc.ReceiptNumber.Should().Be("RC001");
        doc.ReprintLabel.Should().BeNull();
        doc.Lines.Should().Contain(l => l.Text.Contains("Test Shop"));
        doc.Lines.Should().Contain(l => l.Text.Contains("ผัดไทย"));
        doc.Lines.Should().Contain(l => l.Text.Contains("100.00"));
        doc.Lines.Should().Contain(l => l.Text.Contains("Cash"));
    }

    [Fact]
    public void Formatter_FormatCustomerReceipt_WithReprint_Should_IncludeReprintLabel()
    {
        // Arrange
        var config = new ReceiptConfiguration { ShopName = "Test", PaperWidth = 48 };
        var formatter = new ReceiptFormatter(config);
        var data = new CustomerReceiptData
        {
            ReceiptNumber = "RC001",
            PrintedAt = DateTime.UtcNow,
            Items = new List<ReceiptItemData>(),
            Total = 0m
        };

        // Act
        var doc = formatter.FormatCustomerReceipt(data, isReprint: true);

        // Assert
        doc.ReprintLabel.Should().Be("*** REPRINT ***");
        doc.Lines.First().Text.Should().Be("*** REPRINT ***");
        doc.Lines.First().Bold.Should().BeTrue();
    }

    [Fact]
    public void Formatter_FormatKitchenTicket_Should_IncludeItemDetails()
    {
        // Arrange
        var config = new ReceiptConfiguration { ShopName = "Test", PaperWidth = 48 };
        var formatter = new ReceiptFormatter(config);
        var data = new KitchenTicketReceiptData
        {
            TicketNumber = 1,
            PrintedAt = DateTime.UtcNow,
            Items = new List<KitchenReceiptItemData>
            {
                new() { Name = "กะเพราหมู", Quantity = 2, Notes = "ไข่ดาว" }
            }
        };

        // Act
        var doc = formatter.FormatKitchenTicket(data);

        // Assert
        doc.Title.Should().Be("Kitchen Ticket");
        doc.Lines.Should().Contain(l => l.Text.Contains("Ticket #1"));
        doc.Lines.Should().Contain(l => l.Text.Contains("กะเพราหมู"));
        doc.Lines.Should().Contain(l => l.Text.Contains("Note: ไข่ดาว"));
    }

    [Fact]
    public void Formatter_FormatRefundReceipt_Should_IncludeRefundDetails()
    {
        // Arrange
        var config = new ReceiptConfiguration { ShopName = "Test", PaperWidth = 48 };
        var formatter = new ReceiptFormatter(config);
        var data = new RefundReceiptData
        {
            ReceiptNumber = "RF001",
            OriginalReceiptNumber = "RC001",
            PrintedAt = DateTime.UtcNow,
            AmountRefunded = 100m,
            Reason = "ลูกค้าเปลี่ยนใจ",
            PaymentMethod = "Cash"
        };

        // Act
        var doc = formatter.FormatRefundReceipt(data);

        // Assert
        doc.Title.Should().Be("Refund Receipt");
        doc.ReceiptNumber.Should().Be("RF001");
        doc.Lines.Should().Contain(l => l.Text.Contains("Refund"));
        doc.Lines.Should().Contain(l => l.Text.Contains("100.00"));
        doc.Lines.Should().Contain(l => l.Text.Contains("ลูกค้าเปลี่ยนใจ"));
    }

    // ── 5. NullReceiptPrinter ──────────────────────────────────

    [Fact]
    public async Task NullReceiptPrinter_PrintAsync_Should_ReturnSuccessfulResult()
    {
        // Arrange
        var printer = new NullReceiptPrinter();
        var doc = new ReceiptDocument { Title = "Test", Lines = new List<ReceiptLine>() };

        // Act
        var result = await printer.PrintAsync(doc);

        // Assert
        result.Success.Should().BeTrue();
        result.Status.Should().Be("Completed");
        result.StartedAt.Should().NotBe(default);
        result.FinishedAt.Should().NotBe(default);
        result.Duration.Should().BeGreaterThanOrEqualTo(TimeSpan.Zero);
        result.PrinterName.Should().Contain("Null Printer");
    }

    [Fact]
    public async Task NullKitchenPrinter_PrintAsync_Should_ReturnSuccessfulResult()
    {
        // Arrange
        var printer = new NullKitchenPrinter();
        var doc = new ReceiptDocument { Title = "Kitchen", Lines = new List<ReceiptLine>() };

        // Act
        var result = await printer.PrintAsync(doc);

        // Assert
        result.Success.Should().BeTrue();
        result.Status.Should().Be("Completed");
        result.PrinterName.Should().Contain("Null Kitchen");
    }

    // ── 6. Dependency Injection ─────────────────────────────────

    [Fact]
    public void DI_ReceiptFormatter_Should_BeResolvable()
    {
        using var scope = _factory.Services.CreateScope();
        var formatter = scope.ServiceProvider.GetRequiredService<IReceiptFormatter>();
        formatter.Should().NotBeNull();
        formatter.Should().BeOfType<ReceiptFormatter>();
    }

    [Fact]
    public void DI_ReceiptConfiguration_Should_BeResolvable()
    {
        using var scope = _factory.Services.CreateScope();
        var config = scope.ServiceProvider.GetRequiredService<ReceiptConfiguration>();
        config.Should().NotBeNull();
    }

    [Fact]
    public void DI_ReceiptDataProvider_Should_BeResolvable()
    {
        using var scope = _factory.Services.CreateScope();
        var provider = scope.ServiceProvider.GetRequiredService<IReceiptDataProvider>();
        provider.Should().NotBeNull();
    }

    [Fact]
    public void DI_NullReceiptPrinter_Should_BeResolvable()
    {
        using var scope = _factory.Services.CreateScope();
        var printer = scope.ServiceProvider.GetRequiredService<IReceiptPrinter>();
        printer.Should().NotBeNull();
        printer.PrinterName.Should().Contain("Null Printer");
    }

    [Fact]
    public void DI_NullKitchenPrinter_Should_BeResolvable()
    {
        using var scope = _factory.Services.CreateScope();
        var printer = scope.ServiceProvider.GetRequiredService<IKitchenPrinter>();
        printer.Should().NotBeNull();
        printer.PrinterName.Should().Contain("Null Kitchen");
    }

    // ── 7. ReceiptDataProvider ──────────────────────────────────

    [Fact]
    public async Task DataProvider_GetCustomerReceiptDataAsync_Should_ReturnFlatDto()
    {
        // Arrange
        var orderId = await SeedCompletedOrderWithPaymentAsync(150m);
        using var scope = _factory.Services.CreateScope();
        var provider = scope.ServiceProvider.GetRequiredService<IReceiptDataProvider>();

        // Act
        var data = await provider.GetCustomerReceiptDataAsync(orderId);

        // Assert — no Domain types leaked
        data.Should().NotBeNull();
        data!.ReceiptNumber.Should().NotBeNullOrEmpty();
        data.Total.Should().Be(150m);
        data.Items.Should().NotBeEmpty();
        data.PaymentMethod.Should().Be("Cash");
    }

    [Fact]
    public async Task DataProvider_GetKitchenReceiptDataAsync_Should_ReturnFlatDto()
    {
        // Arrange
        var ticketNumber = await SeedKitchenTicketAsync();
        using var scope = _factory.Services.CreateScope();
        var provider = scope.ServiceProvider.GetRequiredService<IReceiptDataProvider>();

        // Act
        var data = await provider.GetKitchenReceiptDataAsync(ticketNumber);

        // Assert
        data.Should().NotBeNull();
        data!.TicketNumber.Should().Be(ticketNumber);
        data.Items.Should().NotBeEmpty();
    }
}