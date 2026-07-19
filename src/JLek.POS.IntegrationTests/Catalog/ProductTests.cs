using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JLek.POS.Application.Features.Catalog.Responses;
using JLek.POS.Domain.Catalog;
using JLek.POS.Infrastructure.Persistence;
using JLek.POS.IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
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

    private async Task<Guid> SeedProductAsync(string name = "Test Product")
    {
        var categoryId = await SeedCategoryAsync();
        var product = Product.Create(name, ProductCategoryId.From(categoryId));

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product.Id.Value;
    }

    private async Task<Guid> SeedUnavailableProductAsync(string name = "Unavail Product")
    {
        var productId = await SeedProductAsync(name);
        var id = ProductId.From(productId);
        using var dbScope = _factory.Services.CreateScope();
        var context = dbScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var product = await context.Products.FindAsync(id);
        product!.SetAvailability(ProductStatus.Unavailable);
        await context.SaveChangesAsync();
        return productId;
    }

    [Fact]
    public async Task CreateProduct_Should_ReturnCreatedProduct()
    {
        var categoryId = await SeedCategoryAsync();
        var request = new { Name = "Chicken Rice", CategoryId = categoryId };

        var response = await _client.PostAsJsonAsync("/products", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await response.Content.ReadFromJsonAsync<ProductResponse>();
        created.Should().NotBeNull();
        created!.Name.Should().Be("Chicken Rice");
        created.CategoryId.Should().Be(categoryId);
        created.Status.Should().Be("Unavailable");
        created.Visibility.Should().Be("CashierOnly");
        created.OptionGroups.Should().BeEmpty();
        created.Modifiers.Should().BeEmpty();
        created.SuggestedPrices.Should().BeEmpty();
        created.IngredientIds.Should().BeEmpty();

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(created.Id));
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("Chicken Rice");
        persisted.Status.Should().Be(ProductStatus.Unavailable);

        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Contain(created.Id.ToString());
    }

    [Fact]
    public async Task CreateProduct_WhenNameIsEmpty_Should_ReturnBadRequest()
    {
        var categoryId = await SeedCategoryAsync();
        var request = new { Name = "", CategoryId = categoryId };

        var response = await _client.PostAsJsonAsync("/products", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetProductById_Should_ReturnProduct()
    {
        var productId = await SeedProductAsync("Tom Yum");

        var response = await _client.GetAsync($"/products/{productId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result.Should().NotBeNull();
        result!.Id.Should().Be(productId);
        result.Name.Should().Be("Tom Yum");
    }

    [Fact]
    public async Task GetProductById_WhenNotFound_Should_ReturnNotFound()
    {
        var response = await _client.GetAsync("/products/00000000-0000-0000-0000-000000000001");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAllProducts_Should_ReturnAllProducts()
    {
        await SeedProductAsync("Pad Thai");
        await SeedProductAsync("Spring Roll");

        var response = await _client.GetAsync("/products");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        products.Should().NotBeNull();
        products!.Count.Should().BeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task UpdateProduct_Should_ReturnUpdatedProduct()
    {
        var productId = await SeedProductAsync("Old Name");

        var updateRequest = new { Name = "New Name", Status = "Unavailable" };
        var response = await _client.PutAsJsonAsync($"/products/{productId}", updateRequest);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await response.Content.ReadFromJsonAsync<ProductResponse>();
        updated.Should().NotBeNull();
        updated!.Name.Should().Be("New Name");
        updated.Status.Should().Be("Unavailable");
    }

    [Fact]
    public async Task DeleteProduct_Should_ReturnNoContent()
    {
        var productId = await SeedProductAsync("To Delete");

        var response = await _client.DeleteAsync($"/products/{productId}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task FullProductLifecycle_Should_Succeed()
    {
        var categoryId = await SeedCategoryAsync();
        var createRequest = new { Name = "Lifecycle Product", CategoryId = categoryId };
        var createResponse = await _client.PostAsJsonAsync("/products", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        created.Should().NotBeNull();
        created!.Id.Should().NotBeEmpty();

        var getResponse = await _client.GetAsync($"/products/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var updateRequest = new { Name = "Updated Lifecycle", Status = "Unavailable" };
        var updateResponse = await _client.PutAsJsonAsync($"/products/{created.Id}", updateRequest);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var deleteResponse = await _client.DeleteAsync($"/products/{created.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetProductsByCategory_Should_ReturnFilteredProducts()
    {
        var categoryId = await SeedCategoryAsync();
        await SeedProductAsync("Product A");

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var category2 = ProductCategory.Create("Category B");
        context.ProductCategories.Add(category2);
        await context.SaveChangesAsync();
        var category2Id = category2.Id.Value;

        var product2 = Product.Create("Product B", ProductCategoryId.From(category2Id));
        context.Products.Add(product2);
        await context.SaveChangesAsync();

        var response = await _client.GetAsync($"/products?categoryId={categoryId}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        products.Should().NotBeNull();
        products!.All(p => p.CategoryId == categoryId).Should().BeTrue();
    }

    [Fact]
    public async Task UpdateProduct_Should_PersistChangesToDatabase()
    {
        var productId = await SeedProductAsync("Original");

        var updateRequest = new { Name = "Updated Name", Status = "Unavailable" };
        var updateResponse = await _client.PutAsJsonAsync($"/products/{productId}", updateRequest);
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(productId));
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("Updated Name");
        persisted.Status.Should().Be(ProductStatus.Unavailable);
    }

    [Fact]
    public async Task DeleteProduct_Should_RemoveFromDatabase()
    {
        var productId = await SeedProductAsync("Delete Me");

        await _client.DeleteAsync($"/products/{productId}");

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var deleted = await context.Products.FindAsync(ProductId.From(productId));
        deleted.Should().BeNull();
    }

    [Fact]
    public async Task CreateProduct_ThenGetById_Should_Match()
    {
        var categoryId = await SeedCategoryAsync();
        var request = new { Name = "Match Product", CategoryId = categoryId };

        var createResponse = await _client.PostAsJsonAsync("/products", request);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await createResponse.Content.ReadFromJsonAsync<ProductResponse>();
        created.Should().NotBeNull();

        var getResponse = await _client.GetAsync($"/products/{created!.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var fetched = await getResponse.Content.ReadFromJsonAsync<ProductResponse>();
        fetched.Should().NotBeNull();
        fetched!.Id.Should().Be(created.Id);
        fetched.Name.Should().Be(created.Name);
        fetched.Status.Should().Be(created.Status);
    }
}