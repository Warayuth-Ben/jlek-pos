using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JLek.POS.Application.Features.Catalog.Responses;
using JLek.POS.Domain.Catalog;
using JLek.POS.Infrastructure.Persistence;
using JLek.POS.IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace JLek.POS.IntegrationTests.Catalog;

[Collection("Catalog")]
public sealed class IngredientTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public IngredientTests(CustomWebApplicationFactory factory)
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

    private async Task<Guid> SeedIngredientAsync(string name = "Test Ingredient")
    {
        var ingredient = Ingredient.Create(name);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Ingredients.Add(ingredient);
        await context.SaveChangesAsync();
        return ingredient.Id.Value;
    }

    // ── Create ──────────────────────────────────────────────────

    [Fact]
    public async Task CreateIngredient_Should_ReturnCreatedIngredient()
    {
        // Act
        var response = await _client.PostAsync("/ingredients?name=Chicken", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<IngredientResponse>();
        result.Should().NotBeNull();
        result!.Name.Should().Be("Chicken");
        result.Status.Should().Be(IngredientStatus.Available);

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Ingredients.FindAsync(IngredientId.From(result.Id.Value));
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("Chicken");

        // Assert — Location header
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Contain(result.Id.Value.ToString());
    }

    // ── Get By Id ───────────────────────────────────────────────

    [Fact]
    public async Task GetIngredientById_Should_ReturnIngredient()
    {
        // Arrange
        var ingredientId = await SeedIngredientAsync("Pork");

        // Act
        var response = await _client.GetAsync($"/ingredients/{ingredientId}");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<IngredientResponse>();
        result.Should().NotBeNull();
        result!.Id.Value.Should().Be(ingredientId);
        result.Name.Should().Be("Pork");
    }

    [Fact]
    public async Task GetIngredientById_WhenNotFound_Should_ReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync($"/ingredients/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // ── Get All ─────────────────────────────────────────────────

    [Fact]
    public async Task GetIngredients_Should_ReturnIngredients()
    {
        // Arrange
        await SeedIngredientAsync("Garlic");
        await SeedIngredientAsync("Pepper");

        // Act
        var response = await _client.GetAsync("/ingredients");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var ingredients = await response.Content.ReadFromJsonAsync<List<IngredientResponse>>();
        ingredients.Should().NotBeNull();
        ingredients.Should().HaveCount(2);
        ingredients!.Any(i => i.Name == "Garlic").Should().BeTrue();
        ingredients!.Any(i => i.Name == "Pepper").Should().BeTrue();
    }

    [Fact]
    public async Task GetIngredients_WhenEmpty_Should_ReturnEmptyList()
    {
        // Act
        var response = await _client.GetAsync("/ingredients");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var ingredients = await response.Content.ReadFromJsonAsync<List<IngredientResponse>>();
        ingredients.Should().NotBeNull();
        ingredients.Should().BeEmpty();
    }

    // ── Rename ──────────────────────────────────────────────────

    [Fact]
    public async Task RenameIngredient_Should_UpdateName()
    {
        // Arrange
        var ingredientId = await SeedIngredientAsync("Old Name");

        // Act
        var response = await _client.PutAsync($"/ingredients/{ingredientId}/rename?name=New+Name", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<IngredientResponse>();
        result.Should().NotBeNull();
        result!.Name.Should().Be("New Name");

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Ingredients.FindAsync(IngredientId.From(ingredientId));
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("New Name");
    }

    [Fact]
    public async Task RenameIngredient_WhenIngredientNotFound_Should_Fail()
    {
        // Act
        var response = await _client.PutAsync($"/ingredients/{Guid.NewGuid()}/rename?name=Any", null);

        // Current: 500. Expected after Global Exception milestone: 404
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ── Set Availability ────────────────────────────────────────

    [Fact]
    public async Task SetAvailability_Should_MarkIngredientAvailable()
    {
        // Arrange — create ingredient (default: Available = 0)
        var ingredientId = await SeedIngredientAsync("Avail Ingredient");

        // First set to Unavailable to verify transition
        await _client.PutAsync($"/ingredients/{ingredientId}/availability?status=1", null);

        // Act — set back to Available (IngredientStatus.Available = 0)
        var response = await _client.PutAsync($"/ingredients/{ingredientId}/availability?status=0", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<IngredientResponse>();
        result.Should().NotBeNull();
        result!.Status.Should().Be(IngredientStatus.Available);

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Ingredients.FindAsync(IngredientId.From(ingredientId));
        persisted!.Status.Should().Be(IngredientStatus.Available);
    }

    [Fact]
    public async Task SetAvailability_Should_MarkIngredientUnavailable()
    {
        // Arrange
        var ingredientId = await SeedIngredientAsync("Unavail Ingredient");

        // Act — set to Unavailable (IngredientStatus.Unavailable = 1)
        var response = await _client.PutAsync($"/ingredients/{ingredientId}/availability?status=1", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<IngredientResponse>();
        result.Should().NotBeNull();
        result!.Status.Should().Be(IngredientStatus.Unavailable);

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Ingredients.FindAsync(IngredientId.From(ingredientId));
        persisted!.Status.Should().Be(IngredientStatus.Unavailable);
    }

    [Fact]
    public async Task SetAvailability_WhenIngredientNotFound_Should_Fail()
    {
        // Act
        var response = await _client.PutAsync($"/ingredients/{Guid.NewGuid()}/availability?status=1", null);

        // Current: 500. Expected after Global Exception milestone: 404
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}