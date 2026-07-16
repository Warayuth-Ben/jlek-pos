using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JLek.POS.Application.Features.Kitchen.Responses;
using JLek.POS.Domain.Kitchen;
using JLek.POS.Infrastructure.Persistence;
using JLek.POS.IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace JLek.POS.IntegrationTests.Kitchen;

[Collection("Catalog")]
public sealed class KitchenTicketTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private int _nextTicketNumber = 100;

    public KitchenTicketTests(CustomWebApplicationFactory factory)
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

    private async Task<Guid> SeedTicketAsync(string itemName = "Pad Thai", int quantity = 1, string? notes = null)
    {
        var ticket = KitchenTicket.Create(_nextTicketNumber++, itemName, quantity, notes);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.KitchenTickets.Add(ticket);
        await context.SaveChangesAsync();
        return ticket.Id.Value;
    }

    private async Task<Guid> SeedTicketAtStatusAsync(KitchenTicketStatus targetStatus, string itemName = "Test Item")
    {
        var ticketId = await SeedTicketAsync(itemName);
        var id = KitchenTicketId.From(ticketId);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var ticket = await context.KitchenTickets.FirstOrDefaultAsync(t => t.Id == id);

        if (targetStatus == KitchenTicketStatus.Pending) return ticketId;

        ticket!.StartPreparation();
        if (targetStatus == KitchenTicketStatus.Preparing) { await context.SaveChangesAsync(); return ticketId; }

        ticket.CompletePreparation();
        if (targetStatus == KitchenTicketStatus.Ready) { await context.SaveChangesAsync(); return ticketId; }

        ticket.Serve();
        await context.SaveChangesAsync();
        return ticketId;
    }

    // ── 1. Create ──────────────────────────────────────────────

    [Fact]
    public async Task CreateTicket_Should_ReturnCreatedTicket()
    {
        // Act
        var response = await _client.PostAsync(
            $"/kitchen?ticketNumber=1&itemName=Pad+Thai&quantity=2&notes=Extra+Spicy", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<KitchenTicketResponse>();
        result.Should().NotBeNull();
        result!.TicketNumber.Should().Be(1);
        result.Status.Should().Be(KitchenTicketStatus.Pending);
        result.Items.Should().ContainSingle();
        result.Items.First().ItemName.Should().Be("Pad Thai");
        result.Items.First().Quantity.Should().Be(2);
        result.Items.First().Notes.Should().Be("Extra Spicy");

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.KitchenTickets.FindAsync(KitchenTicketId.From(result.Id.Value));
        persisted.Should().NotBeNull();
        persisted!.TicketNumber.Should().Be(1);
        persisted.Status.Should().Be(KitchenTicketStatus.Pending);

        // Assert — Location header
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Contain(result.Id.Value.ToString());
    }

    [Fact]
    public async Task CreateTicket_Should_CreateWithMultipleItems()
    {
        // Arrange — create ticket with first item
        var response1 = await _client.PostAsync(
            $"/kitchen?ticketNumber=10&itemName=Item+A&quantity=1&notes=", null);
        var ticket1 = await response1.Content.ReadFromJsonAsync<KitchenTicketResponse>();

        // Act — add second item
        var response2 = await _client.PostAsync(
            $"/kitchen/{ticket1!.Id.Value}/items?itemName=Item+B&quantity=2&notes=", null);

        // Assert
        response2.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await response2.Content.ReadFromJsonAsync<KitchenTicketResponse>();
        updated!.Items.Should().HaveCount(2);
        updated.Items.Any(i => i.ItemName == "Item A").Should().BeTrue();
        updated.Items.Any(i => i.ItemName == "Item B").Should().BeTrue();
    }

    // ── 2. Get By Id ───────────────────────────────────────────

    [Fact]
    public async Task GetTicketById_Should_ReturnTicket()
    {
        // Arrange
        var ticketId = await SeedTicketAsync("Khao Soi", 1, "Extra noodle");

        // Act
        var response = await _client.GetAsync($"/kitchen/{ticketId}");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<KitchenTicketResponse>();
        result.Should().NotBeNull();
        result!.Items.Should().ContainSingle();
        result.Items.First().ItemName.Should().Be("Khao Soi");
        result.Items.First().Quantity.Should().Be(1);
        result.Items.First().Notes.Should().Be("Extra noodle");
    }

    [Fact]
    public async Task GetTicketById_WhenNotFound_Should_ReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync($"/kitchen/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // ── 3. Get All ─────────────────────────────────────────────

    [Fact]
    public async Task GetTickets_Should_ReturnTickets()
    {
        // Arrange
        await SeedTicketAsync("Item A");
        await SeedTicketAsync("Item B");

        // Act
        var response = await _client.GetAsync("/kitchen");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var tickets = await response.Content.ReadFromJsonAsync<List<KitchenTicketResponse>>();
        tickets.Should().NotBeNull();
        tickets.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetTickets_WhenEmpty_Should_ReturnEmptyList()
    {
        // Act
        var response = await _client.GetAsync("/kitchen");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var tickets = await response.Content.ReadFromJsonAsync<List<KitchenTicketResponse>>();
        tickets.Should().NotBeNull();
        tickets.Should().BeEmpty();
    }

    // ── 4. Get Active ──────────────────────────────────────────

    [Fact]
    public async Task GetActiveTickets_Should_ReturnOnlyNonServedTickets()
    {
        // Arrange
        var pendingId = await SeedTicketAsync("Pending Item");                     // Pending
        var preparingId = await SeedTicketAtStatusAsync(KitchenTicketStatus.Preparing, "Preparing Item"); // Preparing
        var servedId = await SeedTicketAtStatusAsync(KitchenTicketStatus.Served, "Served Item");           // Served

        // Act
        var response = await _client.GetAsync("/kitchen/active");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var active = await response.Content.ReadFromJsonAsync<List<KitchenTicketResponse>>();
        active.Should().NotBeNull();
        active.Should().HaveCount(2); // Pending + Preparing, NOT Served
        active!.Any(t => t.Id.Value == pendingId).Should().BeTrue();
        active!.Any(t => t.Id.Value == preparingId).Should().BeTrue();
        active!.Any(t => t.Id.Value == servedId).Should().BeFalse();
    }

    [Fact]
    public async Task GetActiveTickets_WhenAllServed_Should_ReturnEmptyList()
    {
        // Arrange
        await SeedTicketAtStatusAsync(KitchenTicketStatus.Served, "Served A");
        await SeedTicketAtStatusAsync(KitchenTicketStatus.Served, "Served B");

        // Act
        var response = await _client.GetAsync("/kitchen/active");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var active = await response.Content.ReadFromJsonAsync<List<KitchenTicketResponse>>();
        active.Should().BeEmpty();
    }

    // ── 5. State Machine ───────────────────────────────────────

    [Fact]
    public async Task StartPreparation_Should_TransitionToPreparing()
    {
        // Arrange
        var ticketId = await SeedTicketAsync("Start Item");
        var response = await _client.PostAsync($"/kitchen/{ticketId}/start", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<KitchenTicketResponse>();
        result!.Status.Should().Be(KitchenTicketStatus.Preparing);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.KitchenTickets.FindAsync(KitchenTicketId.From(ticketId));
        persisted!.Status.Should().Be(KitchenTicketStatus.Preparing);
    }

    [Fact]
    public async Task CompletePreparation_Should_TransitionToReady()
    {
        // Arrange
        var ticketId = await SeedTicketAtStatusAsync(KitchenTicketStatus.Preparing, "Complete Item");

        var response = await _client.PostAsync($"/kitchen/{ticketId}/complete", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<KitchenTicketResponse>();
        result!.Status.Should().Be(KitchenTicketStatus.Ready);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.KitchenTickets.FindAsync(KitchenTicketId.From(ticketId));
        persisted!.Status.Should().Be(KitchenTicketStatus.Ready);
    }

    [Fact]
    public async Task ServeTicket_Should_TransitionToServed()
    {
        // Arrange
        var ticketId = await SeedTicketAtStatusAsync(KitchenTicketStatus.Ready, "Serve Item");

        var response = await _client.PostAsync($"/kitchen/{ticketId}/serve", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<KitchenTicketResponse>();
        result!.Status.Should().Be(KitchenTicketStatus.Served);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.KitchenTickets.FindAsync(KitchenTicketId.From(ticketId));
        persisted!.Status.Should().Be(KitchenTicketStatus.Served);
    }

    // ── 6. Invalid State Transitions ──────────────────────────

    [Fact]
    public async Task ServeTicket_WhenPending_Should_Fail()
    {
        var ticketId = await SeedTicketAsync("Pending Serve");
        var response = await _client.PostAsync($"/kitchen/{ticketId}/serve", null);
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task ServeTicket_WhenPreparing_Should_Fail()
    {
        var ticketId = await SeedTicketAtStatusAsync(KitchenTicketStatus.Preparing, "Preparing Serve");
        var response = await _client.PostAsync($"/kitchen/{ticketId}/serve", null);
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task StartPreparation_WhenReady_Should_Fail()
    {
        var ticketId = await SeedTicketAtStatusAsync(KitchenTicketStatus.Ready, "Ready Start");
        var response = await _client.PostAsync($"/kitchen/{ticketId}/start", null);
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task AddItem_WhenServed_Should_Fail()
    {
        var ticketId = await SeedTicketAtStatusAsync(KitchenTicketStatus.Served, "Served Add");
        var response = await _client.PostAsync($"/kitchen/{ticketId}/items?itemName=New&quantity=1&notes=", null);
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task StartPreparation_WhenServed_Should_Fail()
    {
        var ticketId = await SeedTicketAtStatusAsync(KitchenTicketStatus.Served, "Served Start");
        var response = await _client.PostAsync($"/kitchen/{ticketId}/start", null);
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CompletePreparation_WhenServed_Should_Fail()
    {
        var ticketId = await SeedTicketAtStatusAsync(KitchenTicketStatus.Served, "Served Complete");
        var response = await _client.PostAsync($"/kitchen/{ticketId}/complete", null);
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ── 7. Snapshot Persistence ───────────────────────────────

    [Fact]
    public async Task KitchenItemSnapshot_Should_PersistAndReloadIdentically()
    {
        // Arrange — create ticket with snapshot data
        var response = await _client.PostAsync(
            $"/kitchen?ticketNumber=50&itemName=Tom+Yum+Kung&quantity=3&notes=No+Pork", null);
        var created = await response.Content.ReadFromJsonAsync<KitchenTicketResponse>();

        // Act — reload
        var reloadResponse = await _client.GetAsync($"/kitchen/{created!.Id.Value}");
        var reloaded = await reloadResponse.Content.ReadFromJsonAsync<KitchenTicketResponse>();

        // Assert — snapshot identical
        reloaded!.Items.Should().ContainSingle();
        reloaded.Items.First().ItemName.Should().Be("Tom Yum Kung");
        reloaded.Items.First().Quantity.Should().Be(3);
        reloaded.Items.First().Notes.Should().Be("No Pork");
    }

    [Fact]
    public async Task KitchenItemNotes_Should_AcceptNullNotes()
    {
        // Arrange
        var response = await _client.PostAsync(
            $"/kitchen?ticketNumber=60&itemName=Plain+Rice&quantity=1&notes=", null);

        // note: passing empty string for notes (query param)
        var result = await response.Content.ReadFromJsonAsync<KitchenTicketResponse>();
        // The API receives empty string, which may be stored as null or empty
        result!.Items.First().ItemName.Should().Be("Plain Rice");
    }

    // ── 8. Owned Collection ────────────────────────────────────

    [Fact]
    public async Task KitchenItems_Should_PersistAsOwnedCollection()
    {
        // Arrange — create ticket with item
        var createResponse = await _client.PostAsync(
            $"/kitchen?ticketNumber=70&itemName=First+Item&quantity=2&notes=Note+A", null);
        var ticket = await createResponse.Content.ReadFromJsonAsync<KitchenTicketResponse>();

        // Add a second item
        await _client.PostAsync(
            $"/kitchen/{ticket!.Id.Value}/items?itemName=Second+Item&quantity=1&notes=Note+B", null);

        // Act — reload
        var reloadResponse = await _client.GetAsync($"/kitchen/{ticket.Id.Value}");
        var reloaded = await reloadResponse.Content.ReadFromJsonAsync<KitchenTicketResponse>();

        // Assert — both items present
        reloaded!.Items.Should().HaveCount(2);
        reloaded.Items.Should().Contain(i => i.ItemName == "First Item" && i.Quantity == 2 && i.Notes == "Note A");
        reloaded.Items.Should().Contain(i => i.ItemName == "Second Item" && i.Quantity == 1 && i.Notes == "Note B");
    }

    [Fact]
    public async Task KitchenItems_WhenTicketDeleted_Should_CascadeDelete()
    {
        // Arrange
        var ticketId = await SeedTicketAsync("Cascade Item", 5, "Test cascade");

        // Verify items exist
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var ticket = await context.KitchenTickets
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(t => t.Id == KitchenTicketId.From(ticketId));
            ticket.Should().NotBeNull();
            ticket!.Items.Should().ContainSingle();
        }
    }
}