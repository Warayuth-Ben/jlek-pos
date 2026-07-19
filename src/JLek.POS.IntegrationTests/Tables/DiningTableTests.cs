using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JLek.POS.Application.Features.Tables.Responses;
using JLek.POS.Domain.Tables;
using JLek.POS.Domain.ValueObjects;
using JLek.POS.Infrastructure.Persistence;
using JLek.POS.IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace JLek.POS.IntegrationTests.Tables;

[Collection("Catalog")]
public sealed class DiningTableTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public DiningTableTests(CustomWebApplicationFactory factory)
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

    private async Task<Guid> SeedTableAsync(string name = "Test Table")
    {
        var table = DiningTable.Create(name);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.DiningTables.Add(table);
        await context.SaveChangesAsync();
        return table.Id.Value;
    }

    private async Task<Guid> SeedOccupiedTableAsync(string name = "Occupied Table")
    {
        var table = DiningTable.Create(name);
        table.Assign(new(Guid.NewGuid()));

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.DiningTables.Add(table);
        await context.SaveChangesAsync();
        return table.Id.Value;
    }

    // ── Create ──────────────────────────────────────────────────

    [Fact]
    public async Task CreateTable_Should_ReturnCreatedTable()
    {
        // Act
        var response = await _client.PostAsync("/tables?name=Table+A", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<DiningTableResponse>();
        result.Should().NotBeNull();
        result!.Name.Should().Be("Table A");
        result.Status.Should().Be("Available");
        result.ActiveSessionId.Should().BeNull();
        result.MergedTableIds.Should().BeEmpty();

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.DiningTables.FindAsync(TableId.From(result.Id));
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("Table A");
        persisted.Status.Should().Be(TableStatus.Available);

        // Assert — Location header
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Contain(result.Id.ToString());
    }

    // ── Get By Id ───────────────────────────────────────────────

    [Fact]
    public async Task GetTableById_Should_ReturnTable()
    {
        // Arrange
        var tableId = await SeedTableAsync("Table B");

        // Act
        var response = await _client.GetAsync($"/tables/{tableId}");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<DiningTableResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().Be(tableId);
        result.Name.Should().Be("Table B");
        result.Status.Should().Be("Available");
    }

    [Fact]
    public async Task GetTableById_WhenNotFound_Should_ReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync($"/tables/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // ── Get All ─────────────────────────────────────────────────

    [Fact]
    public async Task GetTables_Should_ReturnTables()
    {
        // Arrange
        await SeedTableAsync("Table C");
        await SeedTableAsync("Table D");

        // Act
        var response = await _client.GetAsync("/tables");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var tables = await response.Content.ReadFromJsonAsync<List<DiningTableResponse>>();
        tables.Should().NotBeNull();
        tables!.Count.Should().Be(2);
        tables.Any(t => t.Name == "Table C").Should().BeTrue();
        tables.Any(t => t.Name == "Table D").Should().BeTrue();
    }

    [Fact]
    public async Task GetTables_WhenEmpty_Should_ReturnEmptyList()
    {
        // Act
        var response = await _client.GetAsync("/tables");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var tables = await response.Content.ReadFromJsonAsync<List<DiningTableResponse>>();
        tables.Should().NotBeNull();
        tables.Should().BeEmpty();
    }

    // ── Get Available ───────────────────────────────────────────

    [Fact]
    public async Task GetAvailableTables_Should_ReturnOnlyAvailableTables()
    {
        // Arrange
        await SeedTableAsync("Available 1");       // Available
        await SeedOccupiedTableAsync("Occupied 1"); // Occupied
        await SeedTableAsync("Available 2");       // Available

        // Act
        var response = await _client.GetAsync("/tables/available");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var available = await response.Content.ReadFromJsonAsync<List<DiningTableResponse>>();
        available.Should().NotBeNull();
        available.Should().HaveCount(2);
        available!.All(t => t.Status == "Available").Should().BeTrue();
    }

    [Fact]
    public async Task GetAvailableTables_WhenAllOccupied_Should_ReturnEmptyList()
    {
        // Arrange
        await SeedOccupiedTableAsync("Occupied A");
        await SeedOccupiedTableAsync("Occupied B");

        // Act
        var response = await _client.GetAsync("/tables/available");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var available = await response.Content.ReadFromJsonAsync<List<DiningTableResponse>>();
        available.Should().BeEmpty();
    }

    // ── Assign ──────────────────────────────────────────────────

    [Fact]
    public async Task AssignTable_Should_AssignSession()
    {
        // Arrange
        var tableId = await SeedTableAsync("Assignable Table");
        var sessionId = Guid.NewGuid();

        // Act
        var response = await _client.PostAsync(
            $"/tables/{tableId}/assign?sessionId={sessionId}", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<DiningTableResponse>();
        result.Should().NotBeNull();
        result!.Status.Should().Be("Occupied");
        result.ActiveSessionId.Should().NotBeNull();
        result.ActiveSessionId!.Value.Should().Be(sessionId);

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.DiningTables.FindAsync(TableId.From(tableId));
        persisted!.Status.Should().Be(TableStatus.Occupied);
        persisted.ActiveSessionId!.Value.Should().Be(sessionId);
    }

    [Fact]
    public async Task AssignTable_WhenOccupied_Should_Fail()
    {
        // Arrange
        var tableId = await SeedOccupiedTableAsync("Already Occupied");

        // Act
        var response = await _client.PostAsync(
            $"/tables/{tableId}/assign?sessionId={Guid.NewGuid()}", null);

        // Assert — Business Rule: CannotAssignOccupiedTableRule
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ── Transfer ────────────────────────────────────────────────

    [Fact]
    public async Task TransferTable_Should_TransferSession()
    {
        // Arrange
        var sourceId = await SeedOccupiedTableAsync("Source");
        var destId = await SeedTableAsync("Destination");

        // Act
        var response = await _client.PostAsync(
            $"/tables/{sourceId}/transfer?destinationTableId={destId}", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<DiningTableResponse>();
        result.Should().NotBeNull();
        result!.Status.Should().Be("Available");
        result.ActiveSessionId.Should().BeNull();

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var sourcePersisted = await context.DiningTables.FindAsync(TableId.From(sourceId));
        sourcePersisted!.Status.Should().Be(TableStatus.Available);
        sourcePersisted.ActiveSessionId.Should().BeNull();

        var destPersisted = await context.DiningTables.FindAsync(TableId.From(destId));
        destPersisted!.Status.Should().Be(TableStatus.Occupied);
        destPersisted.ActiveSessionId.Should().NotBeNull();
    }

    [Fact]
    public async Task TransferTable_WhenSourceNotFound_Should_Fail()
    {
        // Arrange
        var destId = await SeedTableAsync("Destination");

        // Act
        var response = await _client.PostAsync(
            $"/tables/{Guid.NewGuid()}/transfer?destinationTableId={destId}", null);

        // Assert — Current: 500 (Global Exception not yet implemented)
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ── Merge ───────────────────────────────────────────────────

    [Fact]
    public async Task MergeTables_Should_MergeTables()
    {
        // Arrange
        var primaryId = await SeedTableAsync("Primary");
        var mergeId = await SeedTableAsync("To Merge");
        var sessionId = Guid.NewGuid();

        // Assign primary table first
        await _client.PostAsync($"/tables/{primaryId}/assign?sessionId={sessionId}", null);

        // Act
        var response = await _client.PostAsync(
            $"/tables/{primaryId}/merge?tableToMergeId={mergeId}", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<DiningTableResponse>();
        result.Should().NotBeNull();
        result!.MergedTableIds.Should().ContainSingle();
        result.MergedTableIds.First().Should().Be(mergeId);

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var primaryPersisted = await context.DiningTables.FindAsync(TableId.From(primaryId));
        primaryPersisted!.MergedTableIds.Should().ContainSingle();
        primaryPersisted.MergedTableIds.First().Value.Should().Be(mergeId);
    }

    [Fact]
    public async Task MergeTables_WhenPrimaryNotOccupied_Should_Fail()
    {
        // Arrange
        var primaryId = await SeedTableAsync("Primary (no session)");
        var mergeId = await SeedTableAsync("To Merge");

        // Act — primary has no session
        var response = await _client.PostAsync(
            $"/tables/{primaryId}/merge?tableToMergeId={mergeId}", null);

        // Assert — Current: 500 (Global Exception not yet implemented)
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ── Split ───────────────────────────────────────────────────

    [Fact]
    public async Task SplitTable_Should_SplitMergedTable()
    {
        // Arrange
        var primaryId = await SeedTableAsync("Primary");
        var mergeId = await SeedTableAsync("Merged Table");
        var sessionId = Guid.NewGuid();

        // Assign and merge
        await _client.PostAsync($"/tables/{primaryId}/assign?sessionId={sessionId}", null);
        await _client.PostAsync($"/tables/{primaryId}/merge?tableToMergeId={mergeId}", null);

        // Act
        var response = await _client.PostAsync(
            $"/tables/{primaryId}/split?tableToSplitId={mergeId}", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<DiningTableResponse>();
        result.Should().NotBeNull();
        result!.MergedTableIds.Should().BeEmpty();

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.DiningTables.FindAsync(TableId.From(primaryId));
        persisted!.MergedTableIds.Should().BeEmpty();
    }

    // ── Release ─────────────────────────────────────────────────

    [Fact]
    public async Task ReleaseTable_Should_ReleaseTable()
    {
        // Arrange
        var tableId = await SeedOccupiedTableAsync("To Release");

        // Act
        var response = await _client.PostAsync($"/tables/{tableId}/release", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<DiningTableResponse>();
        result.Should().NotBeNull();
        result!.Status.Should().Be("Available");
        result.ActiveSessionId.Should().BeNull();

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.DiningTables.FindAsync(TableId.From(tableId));
        persisted!.Status.Should().Be(TableStatus.Available);
        persisted.ActiveSessionId.Should().BeNull();
    }

    [Fact]
    public async Task ReleaseTable_WhenAlreadyAvailable_Should_Fail()
    {
        // Arrange — table is already Available
        var tableId = await SeedTableAsync("Already Available");

        // Act
        var response = await _client.PostAsync($"/tables/{tableId}/release", null);

        // Assert — Business Rule: CannotReleaseAvailableTableRule
        // Current: 500 (Global Exception not yet implemented)
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}