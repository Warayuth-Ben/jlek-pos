using JLek.POS.Api.Requests;
using JLek.POS.Application.Features.Catalog.Ingredients.Commands.CreateIngredient;
using JLek.POS.Application.Features.Catalog.Ingredients.Commands.RenameIngredient;
using JLek.POS.Application.Features.Catalog.Ingredients.Commands.SetIngredientAvailability;
using JLek.POS.Application.Features.Catalog.Ingredients.Queries.GetIngredientById;
using JLek.POS.Application.Features.Catalog.Ingredients.Queries.GetIngredients;
using JLek.POS.Application.Features.Catalog.ProductCategories.Commands.CreateProductCategory;
using JLek.POS.Application.Features.Catalog.ProductCategories.Commands.HideProductCategory;
using JLek.POS.Application.Features.Catalog.ProductCategories.Commands.RenameProductCategory;
using JLek.POS.Application.Features.Catalog.ProductCategories.Commands.ReorderProductCategory;
using JLek.POS.Application.Features.Catalog.ProductCategories.Commands.ShowProductCategory;
using JLek.POS.Application.Features.Catalog.ProductCategories.Queries.GetProductCategories;
using JLek.POS.Application.Features.Catalog.ProductCategories.Queries.GetProductCategoryById;
using JLek.POS.Application.Features.Catalog.Products.Commands.AddIngredient;
using JLek.POS.Application.Features.Catalog.Products.Commands.AddModifier;
using JLek.POS.Application.Features.Catalog.Products.Commands.AddOptionGroup;
using JLek.POS.Application.Features.Catalog.Products.Commands.AddSuggestedPrice;
using JLek.POS.Application.Features.Catalog.Products.Commands.ChangeCategory;
using JLek.POS.Application.Features.Catalog.Products.Commands.CreateProduct;
using JLek.POS.Application.Features.Catalog.Products.Commands.RemoveIngredient;
using JLek.POS.Application.Features.Catalog.Products.Commands.RemoveModifier;
using JLek.POS.Application.Features.Catalog.Products.Commands.RemoveOptionGroup;
using JLek.POS.Application.Features.Catalog.Products.Commands.RemoveSuggestedPrice;
using JLek.POS.Application.Features.Catalog.Products.Commands.SetAvailability;
using JLek.POS.Application.Features.Catalog.Products.Commands.SetVisibility;
using JLek.POS.Application.Features.Catalog.Products.Commands.UpdateProductDetails;
using JLek.POS.Application.Features.Catalog.Products.Queries.GetProductById;
using JLek.POS.Application.Features.Catalog.Products.Queries.GetProducts;
using JLek.POS.Application.Features.Catalog.Responses;
using JLek.POS.Domain.Catalog;
using JLek.POS.Domain.Common.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace JLek.POS.Api.Endpoints;

public static class CatalogEndpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(
        this IEndpointRouteBuilder app)
    {
        // ── Products ────────────────────────────────────────────────
        var products = app.MapGroup("/products");

        // POST /products
        products.MapPost("/", async (
            CreateProductRequest request,
            [FromServices] CreateProductCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new CreateProductCommand(
                    request.Name,
                    ProductCategoryId.From(request.CategoryId)),
                cancellationToken);

            return Results.Created(
                $"/products/{response.Id}",
                response);
        });

        // GET /products
        products.MapGet("/", async (
            [FromServices] GetProductsQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var productsList = await handler.Handle(
                new GetProductsQuery(),
                cancellationToken);

            return Results.Ok(productsList);
        });

        // GET /products/{id}
        products.MapGet("/{id:guid}", async (
            Guid id,
            [FromServices] GetProductByIdQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new GetProductByIdQuery(
                    ProductId.From(id)),
                cancellationToken);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        });

        // PUT /products/{id}/details
        products.MapPut("/{id:guid}/details", async (
            Guid id,
            UpdateProductDetailsRequest request,
            [FromServices] UpdateProductDetailsCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new UpdateProductDetailsCommand(
                    ProductId.From(id),
                    request.Name,
                    request.Description,
                    request.DisplayOrder),
                cancellationToken);

            return Results.Ok(response);
        });

        // PUT /products/{id}/category
        products.MapPut("/{id:guid}/category", async (
            Guid id,
            Guid categoryId,
            [FromServices] ChangeCategoryCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new ChangeCategoryCommand(
                    ProductId.From(id),
                    ProductCategoryId.From(categoryId)),
                cancellationToken);

            return Results.Ok(response);
        });

        // PUT /products/{id}/availability
        products.MapPut("/{id:guid}/availability", async (
            Guid id,
            int status,
            [FromServices] SetAvailabilityCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new SetAvailabilityCommand(
                    ProductId.From(id),
                    (ProductStatus)status),
                cancellationToken);

            return Results.Ok(response);
        });

        // PUT /products/{id}/visibility
        products.MapPut("/{id:guid}/visibility", async (
            Guid id,
            int visibility,
            [FromServices] SetVisibilityCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new SetVisibilityCommand(
                    ProductId.From(id),
                    (ProductVisibility)visibility),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /products/{id}/suggested-prices
        products.MapPost("/{id:guid}/suggested-prices", async (
            Guid id,
            decimal amount,
            [FromServices] AddSuggestedPriceCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new AddSuggestedPriceCommand(
                    ProductId.From(id),
                    Money.From(amount)),
                cancellationToken);

            return Results.Ok(response);
        });

        // DELETE /products/{id}/suggested-prices
        products.MapDelete("/{id:guid}/suggested-prices", async (
            Guid id,
            decimal amount,
            [FromServices] RemoveSuggestedPriceCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new RemoveSuggestedPriceCommand(
                    ProductId.From(id),
                    Money.From(amount)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /products/{id}/option-groups
        products.MapPost("/{id:guid}/option-groups", async (
            Guid id,
            string name,
            int min,
            int? max,
            [FromServices] AddOptionGroupCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new AddOptionGroupCommand(
                    ProductId.From(id),
                    name,
                    SelectionRule.Create(min, max)),
                cancellationToken);

            return Results.Ok(response);
        });

        // DELETE /products/{id}/option-groups/{optionGroupId}
        products.MapDelete("/{id:guid}/option-groups/{optionGroupId:guid}", async (
            Guid id,
            Guid optionGroupId,
            [FromServices] RemoveOptionGroupCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new RemoveOptionGroupCommand(
                    ProductId.From(id),
                    OptionGroupId.From(optionGroupId)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /products/{id}/modifiers
        products.MapPost("/{id:guid}/modifiers", async (
            Guid id,
            string name,
            decimal? priceAdjustment,
            [FromServices] AddModifierCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new AddModifierCommand(
                    ProductId.From(id),
                    name,
                    priceAdjustment),
                cancellationToken);

            return Results.Ok(response);
        });

        // DELETE /products/{id}/modifiers/{modifierId}
        products.MapDelete("/{id:guid}/modifiers/{modifierId:guid}", async (
            Guid id,
            Guid modifierId,
            [FromServices] RemoveModifierCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new RemoveModifierCommand(
                    ProductId.From(id),
                    ModifierId.From(modifierId)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /products/{id}/ingredients
        products.MapPost("/{id:guid}/ingredients", async (
            Guid id,
            Guid ingredientId,
            [FromServices] AddIngredientCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new AddIngredientCommand(
                    ProductId.From(id),
                    IngredientId.From(ingredientId)),
                cancellationToken);

            return Results.Ok(response);
        });

        // DELETE /products/{id}/ingredients/{ingredientId}
        products.MapDelete("/{id:guid}/ingredients/{ingredientId:guid}", async (
            Guid id,
            Guid ingredientId,
            [FromServices] RemoveIngredientCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new RemoveIngredientCommand(
                    ProductId.From(id),
                    IngredientId.From(ingredientId)),
                cancellationToken);

            return Results.Ok(response);
        });

        // ── Product Categories ──────────────────────────────────────
        var categories = app.MapGroup("/categories");

        // POST /categories
        categories.MapPost("/", async (
            string name,
            [FromServices] CreateProductCategoryCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new CreateProductCategoryCommand(name),
                cancellationToken);

            return Results.Created(
                $"/categories/{response.Id}",
                response);
        });

        // GET /categories
        categories.MapGet("/", async (
            [FromServices] GetProductCategoriesQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var list = await handler.Handle(
                new GetProductCategoriesQuery(),
                cancellationToken);

            return Results.Ok(list);
        });

        // GET /categories/{id}
        categories.MapGet("/{id:guid}", async (
            Guid id,
            [FromServices] GetProductCategoryByIdQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new GetProductCategoryByIdQuery(
                    ProductCategoryId.From(id)),
                cancellationToken);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        });

        // PUT /categories/{id}/rename
        categories.MapPut("/{id:guid}/rename", async (
            Guid id,
            string name,
            [FromServices] RenameProductCategoryCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new RenameProductCategoryCommand(
                    ProductCategoryId.From(id),
                    name),
                cancellationToken);

            return Results.Ok(response);
        });

        // PUT /categories/{id}/reorder
        categories.MapPut("/{id:guid}/reorder", async (
            Guid id,
            int? displayOrder,
            [FromServices] ReorderProductCategoryCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new ReorderProductCategoryCommand(
                    ProductCategoryId.From(id),
                    displayOrder),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /categories/{id}/hide
        categories.MapPost("/{id:guid}/hide", async (
            Guid id,
            [FromServices] HideProductCategoryCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new HideProductCategoryCommand(
                    ProductCategoryId.From(id)),
                cancellationToken);

            return Results.Ok(response);
        });

        // POST /categories/{id}/show
        categories.MapPost("/{id:guid}/show", async (
            Guid id,
            [FromServices] ShowProductCategoryCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new ShowProductCategoryCommand(
                    ProductCategoryId.From(id)),
                cancellationToken);

            return Results.Ok(response);
        });

        // ── Ingredients ─────────────────────────────────────────────
        var ingredients = app.MapGroup("/ingredients");

        // POST /ingredients
        ingredients.MapPost("/", async (
            string name,
            [FromServices] CreateIngredientCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new CreateIngredientCommand(name),
                cancellationToken);

            return Results.Created(
                $"/ingredients/{response.Id}",
                response);
        });

        // GET /ingredients
        ingredients.MapGet("/", async (
            [FromServices] GetIngredientsQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var list = await handler.Handle(
                new GetIngredientsQuery(),
                cancellationToken);

            return Results.Ok(list);
        });

        // GET /ingredients/{id}
        ingredients.MapGet("/{id:guid}", async (
            Guid id,
            [FromServices] GetIngredientByIdQueryHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new GetIngredientByIdQuery(
                    IngredientId.From(id)),
                cancellationToken);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        });

        // PUT /ingredients/{id}/rename
        ingredients.MapPut("/{id:guid}/rename", async (
            Guid id,
            string name,
            [FromServices] RenameIngredientCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new RenameIngredientCommand(
                    IngredientId.From(id),
                    name),
                cancellationToken);

            return Results.Ok(response);
        });

        // PUT /ingredients/{id}/availability
        ingredients.MapPut("/{id:guid}/availability", async (
            Guid id,
            int status,
            [FromServices] SetIngredientAvailabilityCommandHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                new SetIngredientAvailabilityCommand(
                    IngredientId.From(id),
                    (IngredientStatus)status),
                cancellationToken);

            return Results.Ok(response);
        });

        return app;
    }
}