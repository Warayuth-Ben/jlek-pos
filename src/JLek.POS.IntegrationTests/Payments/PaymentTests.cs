using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JLek.POS.Api.Requests;
using JLek.POS.Application.Features.Payments.Responses;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Payments;
using JLek.POS.Infrastructure.Persistence;
using JLek.POS.IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace JLek.POS.IntegrationTests.Payments;

[Collection("Payments")]
public sealed class PaymentTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public PaymentTests(CustomWebApplicationFactory factory)
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

    private async Task<Guid> SeedOrderAsync(OrderStatus status = OrderStatus.Confirmed)
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var order = Order.Create();
        if (status == OrderStatus.Confirmed)
        {
            order.AddItem(Guid.NewGuid(), Quantity.From(2), Money.From(50m));
            order.Confirm();
        }
        else if (status == OrderStatus.Completed)
        {
            order.AddItem(Guid.NewGuid(), Quantity.From(1), Money.From(100m));
            order.Confirm();
            order.Complete();
        }
        else if (status == OrderStatus.Cancelled)
        {
            order.AddItem(Guid.NewGuid(), Quantity.From(1), Money.From(100m));
            order.Confirm();
            order.Cancel();
        }
        else // Draft
        {
            order.AddItem(Guid.NewGuid(), Quantity.From(1), Money.From(100m));
        }

        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order.Id.Value;
    }

    private async Task<Guid> SeedPaymentAsync(Guid orderId, decimal amountReceived = 100m)
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var order = await context.Orders.FirstAsync(o => o.Id == OrderId.From(orderId));
        var payment = Payment.Create(order, Money.From(amountReceived), PaymentMethod.Cash);
        context.Payments.Add(payment);
        await context.SaveChangesAsync();
        return payment.Id.Value;
    }

    // ── 1. Create Payment ───────────────────────────────────────

    [Fact]
    public async Task CreatePayment_Should_ReturnCreatedPayment()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed);
        var request = new ReceivePaymentRequest(orderId, 100m, (int)PaymentMethod.Cash);

        // Act
        var response = await _client.PostAsJsonAsync("/payments", request);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<PaymentResponse>();
        result.Should().NotBeNull();
        result!.OrderId.Should().Be(orderId);
        result.AmountReceived.Should().Be(100m);
        result.OrderTotal.Should().Be(100m);
        result.Change.Should().Be(0m);
        result.Method.Should().Be("Cash");
        result.Status.Should().Be("Completed");
        result.RefundReason.Should().BeNull();

        // Assert — Location header
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Contain(result.Id.ToString());
    }

    [Fact]
    public async Task CreatePayment_WhenAmountExceeds_Should_CalculateChange()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed); // Total = 100
        var request = new ReceivePaymentRequest(orderId, 120m, (int)PaymentMethod.Cash);

        // Act
        var response = await _client.PostAsJsonAsync("/payments", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var result = await response.Content.ReadFromJsonAsync<PaymentResponse>();
        result!.OrderTotal.Should().Be(100m);
        result.AmountReceived.Should().Be(120m);
        result.Change.Should().Be(20m);
    }

    [Fact]
    public async Task CreatePayment_WhenAmountInsufficient_Should_Reject()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed); // Total = 100
        var request = new ReceivePaymentRequest(orderId, 50m, (int)PaymentMethod.Cash);

        // Act
        var response = await _client.PostAsJsonAsync("/payments", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task CreatePayment_WhenOrderCancelled_Should_Reject()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Cancelled);
        var request = new ReceivePaymentRequest(orderId, 100m, (int)PaymentMethod.Cash);

        // Act
        var response = await _client.PostAsJsonAsync("/payments", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task CreatePayment_WhenOrderCompleted_Should_Reject()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Completed);
        var request = new ReceivePaymentRequest(orderId, 100m, (int)PaymentMethod.Cash);

        // Act
        var response = await _client.PostAsJsonAsync("/payments", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task CreatePayment_WhenOrderNotFound_Should_ReturnError()
    {
        // Arrange
        var request = new ReceivePaymentRequest(Guid.NewGuid(), 100m, (int)PaymentMethod.Cash);

        // Act
        var response = await _client.PostAsJsonAsync("/payments", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    // ── 2. Get Payment By Id ────────────────────────────────────

    [Fact]
    public async Task GetPaymentById_Should_ReturnPayment()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed);
        var paymentId = await SeedPaymentAsync(orderId, 200m);

        // Act
        var response = await _client.GetAsync($"/payments/{paymentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<PaymentResponse>();
        result.Should().NotBeNull();
        result!.OrderId.Should().Be(orderId);
        result.AmountReceived.Should().Be(200m);
        result.Status.Should().Be("Completed");
    }

    [Fact]
    public async Task GetPaymentById_WhenNotFound_Should_ReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync($"/payments/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // ── 3. Get Payments By OrderId ──────────────────────────────

    [Fact]
    public async Task GetPaymentsByOrderId_Should_ReturnPayments()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed);
        await SeedPaymentAsync(orderId, 100m);

        // Act
        var response = await _client.GetAsync($"/payments?orderId={orderId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var payments = await response.Content.ReadFromJsonAsync<List<PaymentResponse>>();
        payments.Should().NotBeNull();
        payments.Should().ContainSingle();
        payments!.First().OrderId.Should().Be(orderId);
    }

    [Fact]
    public async Task GetPaymentsByOrderId_WhenNoPayments_Should_ReturnEmpty()
    {
        // Act
        var response = await _client.GetAsync($"/payments?orderId={Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var payments = await response.Content.ReadFromJsonAsync<List<PaymentResponse>>();
        payments.Should().BeEmpty();
    }

    [Fact]
    public async Task GetPaymentsByOrderId_WhenMissingParam_Should_ReturnError()
    {
        // Act
        var response = await _client.GetAsync("/payments");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    // ── 4. Refund ───────────────────────────────────────────────

    [Fact]
    public async Task RefundPayment_Should_TransitionToRefunded()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed);
        var paymentId = await SeedPaymentAsync(orderId, 100m);
        var request = new RefundPaymentRequest("Customer changed mind");

        // Act
        var response = await _client.PostAsJsonAsync($"/payments/{paymentId}/refund", request);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<PaymentResponse>();
        result.Should().NotBeNull();
        result!.Status.Should().Be("Refunded");
        result.RefundReason.Should().Be("Customer changed mind");

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Payments.FindAsync(PaymentId.From(paymentId));
        persisted!.Status.Should().Be(PaymentStatus.Refunded);
        persisted.RefundReason.Should().Be("Customer changed mind");
    }

    [Fact]
    public async Task RefundPayment_WhenAlreadyRefunded_Should_Reject()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed);
        var paymentId = await SeedPaymentAsync(orderId, 100m);
        var request = new RefundPaymentRequest("First refund");

        // First refund
        await _client.PostAsJsonAsync($"/payments/{paymentId}/refund", request);

        // Act — second refund
        var response = await _client.PostAsJsonAsync($"/payments/{paymentId}/refund",
            new RefundPaymentRequest("Second refund"));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task RefundPayment_WhenNotFound_Should_ReturnError()
    {
        // Arrange
        var request = new RefundPaymentRequest("Test refund");

        // Act
        var response = await _client.PostAsJsonAsync($"/payments/{Guid.NewGuid()}/refund", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    // ── 5. State Machine ────────────────────────────────────────

    [Fact]
    public async Task Payment_AfterRefund_Should_BeTerminal()
    {
        // Arrange — create + refund
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed);
        var paymentId = await SeedPaymentAsync(orderId, 100m);
        await _client.PostAsJsonAsync($"/payments/{paymentId}/refund",
            new RefundPaymentRequest("Test"));

        // Verify status via GET
        var response = await _client.GetAsync($"/payments/{paymentId}");
        var result = await response.Content.ReadFromJsonAsync<PaymentResponse>();
        result!.Status.Should().Be("Refunded");
    }

    // ── 6. Money Persistence ────────────────────────────────────

    [Fact]
    public async Task Payment_PersistsMoneyValuesCorrectly()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed);
        var request = new ReceivePaymentRequest(orderId, 250.50m, (int)PaymentMethod.QR);

        // Act — create
        var createResponse = await _client.PostAsJsonAsync("/payments", request);
        var created = await createResponse.Content.ReadFromJsonAsync<PaymentResponse>();

        // Act — reload
        var reloadResponse = await _client.GetAsync($"/payments/{created!.Id}");
        var reloaded = await reloadResponse.Content.ReadFromJsonAsync<PaymentResponse>();

        // Assert
        reloaded!.OrderTotal.Should().Be(created.OrderTotal);
        reloaded.AmountReceived.Should().Be(250.50m);
        reloaded.Change.Should().Be(created.Change);
        reloaded.Method.Should().Be("QR");
        reloaded.Status.Should().Be("Completed");
    }

    // ── 7. PaymentMethod Persistence ────────────────────────────

    [Fact]
    public async Task Payment_WithCardMethod_Should_Persist()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed);
        var request = new ReceivePaymentRequest(orderId, 100m, (int)PaymentMethod.Card);

        // Act
        var response = await _client.PostAsJsonAsync("/payments", request);
        var created = await response.Content.ReadFromJsonAsync<PaymentResponse>();

        // Assert
        created!.Method.Should().Be("Card");
    }

    [Fact]
    public async Task Payment_WithCreditMethod_Should_Persist()
    {
        // Arrange
        var orderId = await SeedOrderAsync(OrderStatus.Confirmed);
        var request = new ReceivePaymentRequest(orderId, 100m, (int)PaymentMethod.Credit);

        // Act
        var response = await _client.PostAsJsonAsync("/payments", request);
        var created = await response.Content.ReadFromJsonAsync<PaymentResponse>();

        // Assert
        created!.Method.Should().Be("Credit");
    }
}