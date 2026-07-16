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
public sealed class ProductCategoryTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public ProductCategoryTests(CustomWebApplicationFactory factory)
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

    private async Task<Guid> SeedCategoryAsync(string name = "Test Category")
    {
        var category = ProductCategory.Create(name);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.ProductCategories.Add(category);
        await context.SaveChangesAsync();
        return category.Id.Value;
    }

    // ── Create ──────────────────────────────────────────────────

    [Fact]
    public async Task CreateCategory_Should_ReturnCreatedCategory()
    {
        // Act
        var response = await _client.PostAsync("/categories?name=Drinks", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
        result.Should().NotBeNull();
        result!.Name.Should().Be("Drinks");
        result.Status.Should().Be(ProductCategoryStatus.Available);

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.ProductCategories.FindAsync(ProductCategoryId.From(result.Id.Value));
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("Drinks");

        // Assert — Location header
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Contain(result.Id.Value.ToString());
    }

    // ── Get By Id ───────────────────────────────────────────────

    [Fact]
    public async Task GetCategoryById_Should_ReturnCategory()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync("Rice");

        // Act
        var response = await _client.GetAsync($"/categories/{categoryId}");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
        result.Should().NotBeNull();
        result!.Id.Value.Should().Be(categoryId);
        result.Name.Should().Be("Rice");
    }

    [Fact]
    public async Task GetCategoryById_WhenNotFound_Should_ReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync($"/categories/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // ── Get All ─────────────────────────────────────────────────

    [Fact]
    public async Task GetCategories_Should_ReturnCategories()
    {
        // Arrange
        await SeedCategoryAsync("Soup");
        await SeedCategoryAsync("Drinks");

        // Act
        var response = await _client.GetAsync("/categories");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var categories = await response.Content.ReadFromJsonAsync<List<ProductCategoryResponse>>();
        categories.Should().NotBeNull();
        categories.Should().HaveCount(2);
        categories!.Any(c => c.Name == "Soup").Should().BeTrue();
        categories!.Any(c => c.Name == "Drinks").Should().BeTrue();
    }

    [Fact]
    public async Task GetCategories_WhenEmpty_Should_ReturnEmptyList()
    {
        // Act
        var response = await _client.GetAsync("/categories");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var categories = await response.Content.ReadFromJsonAsync<List<ProductCategoryResponse>>();
        categories.Should().NotBeNull();
        categories.Should().BeEmpty();
    }

    // ── Rename ──────────────────────────────────────────────────

    [Fact]
    public async Task RenameCategory_Should_UpdateName()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync("Old Name");

        // Act
        var response = await _client.PutAsync($"/categories/{categoryId}/rename?name=New+Name", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
        result.Should().NotBeNull();
        result!.Name.Should().Be("New Name");

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.ProductCategories.FindAsync(ProductCategoryId.From(categoryId));
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("New Name");
    }

    [Fact]
    public async Task RenameCategory_WhenCategoryNotFound_Should_Fail()
    {
        // Act
        var response = await _client.PutAsync($"/categories/{Guid.NewGuid()}/rename?name=Any", null);

        // Current: 500. Expected after Global Exception milestone: 404
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ── Reorder ─────────────────────────────────────────────────

    [Fact]
    public async Task ReorderCategory_Should_UpdateDisplayOrder()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync("Reorder Cat");

        // Act
        var response = await _client.PutAsync($"/categories/{categoryId}/reorder?displayOrder=3", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
        result.Should().NotBeNull();
        result!.DisplayOrder.Should().Be(3);

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.ProductCategories.FindAsync(ProductCategoryId.From(categoryId));
        persisted!.DisplayOrder.Should().Be(3);
    }

    [Fact]
    public async Task ReorderCategory_Should_AcceptNullDisplayOrder()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync("Null Order");
        // First set to a value
        await _client.PutAsync($"/categories/{categoryId}/reorder?displayOrder=5", null);

        // Act — set to null
        var response = await _client.PutAsync($"/categories/{categoryId}/reorder", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
        result!.DisplayOrder.Should().BeNull();
    }

    // ── Hide ────────────────────────────────────────────────────

    [Fact]
    public async Task HideCategory_Should_HideCategory()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync("Visible Cat");

        // Act
        var response = await _client.PostAsync($"/categories/{categoryId}/hide", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
        result.Should().NotBeNull();
        result!.Status.Should().Be(ProductCategoryStatus.Hidden);

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.ProductCategories.FindAsync(ProductCategoryId.From(categoryId));
        persisted!.Status.Should().Be(ProductCategoryStatus.Hidden);
    }

    // ── Show ────────────────────────────────────────────────────

    [Fact]
    public async Task ShowCategory_Should_ShowCategory()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync("Hidden Cat");

        // First hide it
        await _client.PostAsync($"/categories/{categoryId}/hide", null);

        // Act — then show it
        var response = await _client.PostAsync($"/categories/{categoryId}/show", null);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<ProductCategoryResponse>();
        result.Should().NotBeNull();
        result!.Status.Should().Be(ProductCategoryStatus.Available);

        // Assert — Database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.ProductCategories.FindAsync(ProductCategoryId.From(categoryId));
        persisted!.Status.Should().Be(ProductCategoryStatus.Available);
    }
}