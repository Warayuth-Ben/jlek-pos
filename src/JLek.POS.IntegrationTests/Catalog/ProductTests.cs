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
        created.CategoryId.Value.Should().Be(categoryId);
        created.Status.Should().Be(ProductStatus.Unavailable);
        created.Visibility.Should().Be(ProductVisibility.CashierOnly);
        created.OptionGroups.Should().BeEmpty();
        created.Modifiers.Should().BeEmpty();
        created.SuggestedPrices.Should().BeEmpty();
        created.IngredientIds.Should().BeEmpty();

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(created.Id);
        persisted.Should().NotBeNull();
        persisted!.Name.Should().Be("Chicken Rice");
        persisted.Status.Should().Be(ProductStatus.Unavailable);

        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.ToString().Should().Contain(created.Id.Value.ToString());
    }

    [Fact]
    public async Task GetProductById_Should_ReturnProduct()
    {
        var categoryId = await SeedCategoryAsync();
        var product = Product.Create("Crispy Pork Rice", ProductCategoryId.From(categoryId));
        product.UpdateDetails("Crispy Pork Rice", "With extra sauce", 1);

        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        var response = await _client.GetAsync($"/products/{product.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
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
        var response = await _client.GetAsync($"/products/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetProducts_Should_ReturnProducts()
    {
        var categoryId = await SeedCategoryAsync();
        var product1 = Product.Create("Product A", ProductCategoryId.From(categoryId));
        var product2 = Product.Create("Product B", ProductCategoryId.From(categoryId));

        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Products.AddRange(product1, product2);
            await context.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/products");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        products.Should().NotBeNull();
        products.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetProducts_WhenEmpty_Should_ReturnEmptyList()
    {
        var response = await _client.GetAsync("/products");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        products.Should().NotBeNull();
        products.Should().BeEmpty();
    }

    // ── Update Details ──────────────────────────────────────────

    [Fact]
    public async Task UpdateProductDetails_Should_UpdateProduct()
    {
        var productId = await SeedProductAsync("Original Name");
        var request = new { Name = "Updated Name", Description = "Updated Description", DisplayOrder = 5 };

        var response = await _client.PutAsJsonAsync($"/products/{productId}/details", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.Name.Should().Be("Updated Name");
        result.Description.Should().Be("Updated Description");
        result.DisplayOrder.Should().Be(5);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(productId));
        persisted!.Name.Should().Be("Updated Name");
        persisted.Description.Should().Be("Updated Description");
        persisted.DisplayOrder.Should().Be(5);
    }

    [Fact]
    public async Task UpdateProductDetails_WhenProductNotFound_Should_Fail()
    {
        var request = new { Name = "Any Name", Description = (string?)null, DisplayOrder = (int?)null };

        var response = await _client.PutAsJsonAsync($"/products/{Guid.NewGuid()}/details", request);

        // Current: 500. Expected after Global Exception milestone: 404
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task UpdateProductDetails_Should_AcceptNullDescriptionAndDisplayOrder()
    {
        var productId = await SeedProductAsync("Nullable Test");
        var request = new { Name = "Nullable Updated", Description = (string?)null, DisplayOrder = (int?)null };

        var response = await _client.PutAsJsonAsync($"/products/{productId}/details", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.Name.Should().Be("Nullable Updated");
        result.Description.Should().BeNull();
        result.DisplayOrder.Should().BeNull();
    }

    // ── Change Category ─────────────────────────────────────────

    [Fact]
    public async Task ChangeCategory_Should_MoveProductToNewCategory()
    {
        var productId = await SeedProductAsync("Movable Product");
        var newCategoryId = await SeedCategoryAsync();

        var response = await _client.PutAsync($"/products/{productId}/category?categoryId={newCategoryId}", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.CategoryId.Value.Should().Be(newCategoryId);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(productId));
        persisted!.CategoryId.Value.Should().Be(newCategoryId);
    }

    [Fact]
    public async Task ChangeCategory_WhenProductNotFound_Should_Fail()
    {
        var newCategoryId = await SeedCategoryAsync();
        var response = await _client.PutAsync($"/products/{Guid.NewGuid()}/category?categoryId={newCategoryId}", null);

        // Current: 500. Expected after Global Exception milestone: 404
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ── Set Availability ────────────────────────────────────────

    [Fact]
    public async Task SetAvailability_Should_MarkProductAvailable()
    {
        var productId = await SeedProductAsync("Avail Product");
        var response = await _client.PutAsync($"/products/{productId}/availability?status=1", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.Status.Should().Be(ProductStatus.Available);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(productId));
        persisted!.Status.Should().Be(ProductStatus.Available);
    }

    [Fact]
    public async Task SetAvailability_Should_MarkProductUnavailable()
    {
        var productId = await SeedProductAsync("Unavail Product");
        await _client.PutAsync($"/products/{productId}/availability?status=1", null);

        var response = await _client.PutAsync($"/products/{productId}/availability?status=0", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.Status.Should().Be(ProductStatus.Unavailable);
    }

    [Fact]
    public async Task SetAvailability_WhenProductNotFound_Should_Fail()
    {
        var response = await _client.PutAsync($"/products/{Guid.NewGuid()}/availability?status=1", null);

        // Current: 500. Expected after Global Exception milestone: 404
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ── Set Visibility ──────────────────────────────────────────

    [Fact]
    public async Task SetVisibility_Should_UpdateVisibility()
    {
        var productId = await SeedProductAsync("Visible Product");
        var response = await _client.PutAsync($"/products/{productId}/visibility?visibility=2", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.Visibility.Should().Be(ProductVisibility.DeliveryPlatform);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(productId));
        persisted!.Visibility.Should().Be(ProductVisibility.DeliveryPlatform);
    }

    [Fact]
    public async Task SetVisibility_WhenProductNotFound_Should_Fail()
    {
        var response = await _client.PutAsync($"/products/{Guid.NewGuid()}/visibility?visibility=1", null);

        // Current: 500. Expected after Global Exception milestone: 404
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ═══════════════════════════════════════════════════════════════
    // Product Composition Tests — Suggested Price
    // ═══════════════════════════════════════════════════════════════

    [Fact]
    public async Task AddSuggestedPrice_Should_AddPrice()
    {
        var productId = await SeedProductAsync("Price Product");
        var response = await _client.PostAsync($"/products/{productId}/suggested-prices?amount=99.00", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.SuggestedPrices.Should().ContainSingle();
        result.SuggestedPrices.First().Amount.Should().Be(99.00m);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(productId));
        persisted!.SuggestedPrices.Should().ContainSingle(m => m.Amount == 99.00m);
    }

    [Fact]
    public async Task RemoveSuggestedPrice_Should_RemovePrice()
    {
        var productId = await SeedProductAsync("Remove Price");
        await _client.PostAsync($"/products/{productId}/suggested-prices?amount=99.00", null);

        var response = await _client.DeleteAsync($"/products/{productId}/suggested-prices?amount=99.00");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.SuggestedPrices.Should().BeEmpty();

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(productId));
        persisted!.SuggestedPrices.Should().BeEmpty();
    }

    [Fact]
    public async Task AddSuggestedPrice_WhenProductUnavailable_Should_Fail()
    {
        var productId = await SeedUnavailableProductAsync();
        var response = await _client.PostAsync($"/products/{productId}/suggested-prices?amount=99.00", null);

        // Business Rule: CannotModifyUnavailableProductRule
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task RemoveSuggestedPrice_WhenProductUnavailable_Should_Fail()
    {
        var productId = await SeedUnavailableProductAsync("Unavail Price");
        var response = await _client.DeleteAsync($"/products/{productId}/suggested-prices?amount=99.00");

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ═══════════════════════════════════════════════════════════════
    // Product Composition Tests — Option Group
    // ═══════════════════════════════════════════════════════════════

    [Fact]
    public async Task AddOptionGroup_Should_AddOptionGroup()
    {
        var productId = await SeedProductAsync("Option Product");
        var response = await _client.PostAsync($"/products/{productId}/option-groups?name=Size&min=1&max=1", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.OptionGroups.Should().ContainSingle();
        result.OptionGroups.First().Name.Should().Be("Size");
        result.OptionGroups.First().Options.Should().BeEmpty();

        using var dbScope = _factory.Services.CreateScope();
        var ctx = dbScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await ctx.Products.Include(p => p.OptionGroups).FirstOrDefaultAsync(p => p.Id == ProductId.From(productId));
        persisted!.OptionGroups.Should().ContainSingle(g => g.Name == "Size");
    }

    [Fact]
    public async Task RemoveOptionGroup_Should_RemoveOptionGroup()
    {
        var categoryId = await SeedCategoryAsync();
        var product = Product.Create("Option Remove", ProductCategoryId.From(categoryId));
        product.AddOptionGroup("Size", SelectionRule.Create(1, 1));

        Guid optionGroupId;
        using (var dbScope = _factory.Services.CreateScope())
        {
            var ctx = dbScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            ctx.Products.Add(product);
            await ctx.SaveChangesAsync();
            optionGroupId = product.OptionGroups.First().Id.Value;
        }

        var response = await _client.DeleteAsync($"/products/{product.Id.Value}/option-groups/{optionGroupId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.OptionGroups.Should().BeEmpty();

        using var dbScope2 = _factory.Services.CreateScope();
        var ctx2 = dbScope2.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await ctx2.Products.Include(p => p.OptionGroups).FirstOrDefaultAsync(p => p.Id == product.Id);
        persisted!.OptionGroups.Should().BeEmpty();
    }

    [Fact]
    public async Task AddOptionGroup_WhenProductUnavailable_Should_Fail()
    {
        var productId = await SeedUnavailableProductAsync();
        var response = await _client.PostAsync($"/products/{productId}/option-groups?name=Size&min=1&max=1", null);

        // Business Rule: CannotModifyUnavailableProductRule
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ═══════════════════════════════════════════════════════════════
    // Product Composition Tests — Modifier
    // ═══════════════════════════════════════════════════════════════

    [Fact]
    public async Task AddModifier_Should_AddModifier()
    {
        var productId = await SeedProductAsync("Modifier Product");
        var response = await _client.PostAsync($"/products/{productId}/modifiers?name=Add+Egg&priceAdjustment=10.00", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.Modifiers.Should().ContainSingle();
        result.Modifiers.First().Name.Should().Be("Add Egg");

        using var dbScope = _factory.Services.CreateScope();
        var ctx = dbScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await ctx.Products.Include(p => p.Modifiers).FirstOrDefaultAsync(p => p.Id == ProductId.From(productId));
        persisted!.Modifiers.Should().ContainSingle(m => m.Name == "Add Egg");
    }

    [Fact]
    public async Task RemoveModifier_Should_RemoveModifier()
    {
        var categoryId = await SeedCategoryAsync();
        var product = Product.Create("Modifier Remove", ProductCategoryId.From(categoryId));
        product.AddModifier("Extra Cheese", 5.00m);

        Guid modifierId;
        using (var dbScope = _factory.Services.CreateScope())
        {
            var ctx = dbScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            ctx.Products.Add(product);
            await ctx.SaveChangesAsync();
            modifierId = product.Modifiers.First().Id.Value;
        }

        var response = await _client.DeleteAsync($"/products/{product.Id.Value}/modifiers/{modifierId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.Modifiers.Should().BeEmpty();

        using var dbScope2 = _factory.Services.CreateScope();
        var ctx2 = dbScope2.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await ctx2.Products.Include(p => p.Modifiers).FirstOrDefaultAsync(p => p.Id == product.Id);
        persisted!.Modifiers.Should().BeEmpty();
    }

    [Fact]
    public async Task AddModifier_WhenProductUnavailable_Should_Fail()
    {
        var productId = await SeedUnavailableProductAsync();
        var response = await _client.PostAsync($"/products/{productId}/modifiers?name=Add+Egg&priceAdjustment=10.00", null);

        // Business Rule: CannotModifyUnavailableProductRule
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    // ═══════════════════════════════════════════════════════════════
    // Product Composition Tests — Ingredient Reference
    // ═══════════════════════════════════════════════════════════════

    [Fact]
    public async Task AddIngredient_Should_AddIngredientReference()
    {
        var productId = await SeedProductAsync("Ingredient Product");
        var ingredientId = Guid.NewGuid();

        var response = await _client.PostAsync($"/products/{productId}/ingredients?ingredientId={ingredientId}", null);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.IngredientIds.Should().ContainSingle();
        result.IngredientIds.First().Value.Should().Be(ingredientId);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(productId));
        persisted!.IngredientIds.Should().ContainSingle(i => i.Value == ingredientId);
    }

    [Fact]
    public async Task RemoveIngredient_Should_RemoveIngredientReference()
    {
        var productId = await SeedProductAsync("Ingredient Remove");
        var ingredientId = Guid.NewGuid();

        await _client.PostAsync($"/products/{productId}/ingredients?ingredientId={ingredientId}", null);
        var response = await _client.DeleteAsync($"/products/{productId}/ingredients/{ingredientId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ProductResponse>();
        result!.IngredientIds.Should().BeEmpty();

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var persisted = await context.Products.FindAsync(ProductId.From(productId));
        persisted!.IngredientIds.Should().BeEmpty();
    }

    [Fact]
    public async Task AddIngredient_WhenProductUnavailable_Should_Fail()
    {
        var productId = await SeedUnavailableProductAsync("Unavail Ingredient");
        var response = await _client.PostAsync($"/products/{productId}/ingredients?ingredientId={Guid.NewGuid()}", null);

        // Business Rule: CannotModifyUnavailableProductRule
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}