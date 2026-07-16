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
public sealed class ProductTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public ProductTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        // Ensure database is created and migrations are applied
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    private async Task<Guid> SeedCategoryAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var category = ProductCategory.Create("Test Category");
        context.ProductCategories.Add(category);
        await context.SaveChangesAsync();
        return category.Id.Value;
    }

    [Fact]
    public async Task CreateProduct_Should_ReturnCreatedProduct()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();

        var request = new
        {
            Name = "Chicken Rice",
            CategoryId = categoryId
        };

        // Act
        var response = await _client.PostAsJsonAsync("/products", request);

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        // Assert — Response DTO
        var created = await response.Content.ReadFromJsonAsync<ProductResponse>();
        created.Should().NotBeNull();
        created!.Name.Should().Be("Chicken Rice");
        created.CategoryId.Value.Should().Be(categoryId);
        created.Status.Should().Be(ProductStatus.Unavailable);
        created.Visibility.Should().Be(ProductVisibility.CashierOnly);
        created.OptionGroups.Should().BeEmpty();
        created.Modifiers.Should().BeEmpty();
        created.SuggestedPrices.Should().BeEmpty();
        created.IngredientIds.Should().BeEmpty();

        // Assert — Persisted in database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(created.Id);
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("Chicken Rice");
        persisted.Status.Should().Be(ProductStatus.Unavailable);

        // Assert — Location header
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Contain(created.Id.Value.ToString());
    }

    [Fact]
    public async Task GetProductById_Should_ReturnProduct()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();
        var product = Product.Create("Crispy Pork Rice", ProductCategoryId.From(categoryId));
        product.UpdateDetails("Crispy Pork Rice", "With extra sauce", 1);

        // Persist directly
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync($"/products/{product.Id.Value}");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result.Should().NotBeNull();
        result!.Id.Value.Should().Be(product.Id.Value);
        result.Name.Should().Be("Crispy Pork Rice");
        result.Description.Should().Be("With extra sauce");
        result.CategoryId.Value.Should().Be(categoryId);
    }

    [Fact]
    public async Task GetProductById_WhenNotFound_Should_ReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync($"/products/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetProducts_Should_ReturnProducts()
    {
        // Arrange
        var categoryId = await SeedCategoryAsync();

        var product1 = Product.Create("Product A", ProductCategoryId.From(categoryId));
        var product2 = Product.Create("Product B", ProductCategoryId.From(categoryId));

        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Products.AddRange(product1, product2);
            await context.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("/products");

        // Assert — HTTP
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert — Response DTO
        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        products.Should().NotBeNull();
        products.Should().HaveCount(2);
        products!.Any(p => p.Id.Value == product1.Id.Value).Should().BeTrue();
        products!.Any(p => p.Id.Value == product2.Id.Value).Should().BeTrue();
    }

    [Fact]
    public async Task GetProducts_WhenEmpty_Should_ReturnEmptyList()
    {
        // Act
        var response = await _client.GetAsync("/products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        products.Should().NotBeNull();
        products.Should().BeEmpty();
    }
}