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
using JLek.POS.Application.Features.Orders.Commands.AddItem;
using JLek.POS.Application.Features.Orders.Commands.CancelOrder;
using JLek.POS.Application.Features.Orders.Commands.CompleteOrder;
using JLek.POS.Application.Features.Orders.Commands.ConfirmOrder;
using JLek.POS.Application.Features.Orders.Commands.CreateOrder;
using JLek.POS.Application.Features.Orders.Commands.RemoveItem;
using JLek.POS.Application.Features.Orders.Queries.GetOrderById;
using JLek.POS.Application.Features.Orders.Queries.GetOrders;
using JLek.POS.Application.Features.Tables.Commands.AssignTable;
using JLek.POS.Application.Features.Tables.Commands.CreateDiningTable;
using JLek.POS.Application.Features.Tables.Commands.MergeTables;
using JLek.POS.Application.Features.Tables.Commands.OpenTable;
using JLek.POS.Application.Features.Tables.Commands.ReleaseTable;
using JLek.POS.Application.Features.Tables.Commands.SplitTable;
using JLek.POS.Application.Features.Tables.Commands.TransferTable;
using JLek.POS.Application.Features.Tables.Queries.GetAvailableDiningTables;
using JLek.POS.Application.Features.Tables.Queries.GetDiningTableById;
using JLek.POS.Application.Features.Kitchen.Commands.AddKitchenItem;
using JLek.POS.Application.Features.Kitchen.Commands.CompletePreparation;
using JLek.POS.Application.Features.Kitchen.Commands.CreateKitchenTicket;
using JLek.POS.Application.Features.Kitchen.Commands.ServeKitchenTicket;
using JLek.POS.Application.Features.Kitchen.Commands.StartPreparation;
using JLek.POS.Application.Features.Kitchen.Queries.GetActiveKitchenTickets;
using JLek.POS.Application.Features.Kitchen.Queries.GetKitchenTicketById;
using JLek.POS.Application.Features.Kitchen.Queries.GetKitchenTickets;
using JLek.POS.Application.Features.Tables.Queries.GetDiningTables;
using JLek.POS.Application.Features.Payments.Commands.ReceivePayment;
using JLek.POS.Application.Features.Payments.Commands.RefundPayment;
using JLek.POS.Application.Features.Payments.Queries.GetPaymentById;
using JLek.POS.Application.Features.Payments.Queries.GetPaymentsByOrderId;
using JLek.POS.Application.Features.Receipt.Commands.PrintCustomerReceipt;
using JLek.POS.Application.Features.Receipt.Commands.PrintKitchenTicket;
using JLek.POS.Application.Features.Receipt.Commands.PrintRefundReceipt;
using JLek.POS.Application.Features.Orders.EventHandlers;
using JLek.POS.Application.Features.Health.Queries.GetHealth;
using JLek.POS.Application.Features.Receipt.Configuration;
using JLek.POS.Application.Features.Receipt.Services;
using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Abstractions.EventHandling;
using JLek.POS.Application.Features.Reports.Queries.BestSellers;
using JLek.POS.Application.Features.Reports.Queries.DailySales;
using JLek.POS.Application.Features.Reports.Queries.SalesByPayment;
using JLek.POS.Domain.Orders.Events;
using Microsoft.Extensions.DependencyInjection;

namespace JLek.POS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Order commands
        services.AddScoped<CreateOrderCommandHandler>();
        services.AddScoped<AddItemCommandHandler>();
        services.AddScoped<RemoveItemCommandHandler>();
        services.AddScoped<ConfirmOrderCommandHandler>();
        services.AddScoped<CompleteOrderCommandHandler>();
        services.AddScoped<CancelOrderCommandHandler>();

        // Order queries
        services.AddScoped<GetOrderByIdQueryHandler>();
        services.AddScoped<GetOrdersQueryHandler>();

        // Product commands
        services.AddScoped<CreateProductCommandHandler>();
        services.AddScoped<UpdateProductDetailsCommandHandler>();
        services.AddScoped<ChangeCategoryCommandHandler>();
        services.AddScoped<SetAvailabilityCommandHandler>();
        services.AddScoped<SetVisibilityCommandHandler>();
        services.AddScoped<AddSuggestedPriceCommandHandler>();
        services.AddScoped<RemoveSuggestedPriceCommandHandler>();
        services.AddScoped<AddOptionGroupCommandHandler>();
        services.AddScoped<RemoveOptionGroupCommandHandler>();
        services.AddScoped<AddModifierCommandHandler>();
        services.AddScoped<RemoveModifierCommandHandler>();
        services.AddScoped<AddIngredientCommandHandler>();
        services.AddScoped<RemoveIngredientCommandHandler>();

        // Product queries
        services.AddScoped<GetProductByIdQueryHandler>();
        services.AddScoped<GetProductsQueryHandler>();

        // Product category commands
        services.AddScoped<CreateProductCategoryCommandHandler>();
        services.AddScoped<RenameProductCategoryCommandHandler>();
        services.AddScoped<ReorderProductCategoryCommandHandler>();
        services.AddScoped<HideProductCategoryCommandHandler>();
        services.AddScoped<ShowProductCategoryCommandHandler>();

        // Product category queries
        services.AddScoped<GetProductCategoryByIdQueryHandler>();
        services.AddScoped<GetProductCategoriesQueryHandler>();

        // Ingredient commands
        services.AddScoped<CreateIngredientCommandHandler>();
        services.AddScoped<RenameIngredientCommandHandler>();
        services.AddScoped<SetIngredientAvailabilityCommandHandler>();

        // Ingredient queries
        services.AddScoped<GetIngredientByIdQueryHandler>();
        services.AddScoped<GetIngredientsQueryHandler>();

        // Table commands
        services.AddScoped<CreateDiningTableCommandHandler>();
        services.AddScoped<OpenTableCommandHandler>();
        services.AddScoped<AssignTableCommandHandler>();
        services.AddScoped<TransferTableCommandHandler>();
        services.AddScoped<MergeTablesCommandHandler>();
        services.AddScoped<SplitTableCommandHandler>();
        services.AddScoped<ReleaseTableCommandHandler>();

        // Table queries
        services.AddScoped<GetDiningTableByIdQueryHandler>();
        services.AddScoped<GetDiningTablesQueryHandler>();
        services.AddScoped<GetAvailableDiningTablesQueryHandler>();

        // Kitchen commands
        services.AddScoped<CreateKitchenTicketCommandHandler>();
        services.AddScoped<AddKitchenItemCommandHandler>();
        services.AddScoped<StartPreparationCommandHandler>();
        services.AddScoped<CompletePreparationCommandHandler>();
        services.AddScoped<ServeKitchenTicketCommandHandler>();

        // Kitchen queries
        services.AddScoped<GetKitchenTicketByIdQueryHandler>();
        services.AddScoped<GetKitchenTicketsQueryHandler>();
        services.AddScoped<GetActiveKitchenTicketsQueryHandler>();

        // Payment commands
        services.AddScoped<ReceivePaymentCommandHandler>();
        services.AddScoped<RefundPaymentCommandHandler>();

        // Payment queries
        services.AddScoped<GetPaymentByIdQueryHandler>();
        services.AddScoped<GetPaymentsByOrderIdQueryHandler>();

        // Report queries
        services.AddScoped<GetDailySalesReportQueryHandler>();
        services.AddScoped<GetSalesByPaymentReportQueryHandler>();
        services.AddScoped<GetBestSellerReportQueryHandler>();

        // Health
        services.AddScoped<GetHealthQueryHandler>();

        // Receipt
        services.AddScoped<PrintCustomerReceiptCommandHandler>();
        services.AddScoped<PrintKitchenTicketCommandHandler>();
        services.AddScoped<PrintRefundReceiptCommandHandler>();

        // Event Handlers
        services.AddScoped<
            IDomainEventHandler<OrderConfirmedEvent>,
            OrderConfirmedEventHandler>();

        // Receipt services
        services.AddScoped<IReceiptFormatter, ReceiptFormatter>();
        services.AddScoped<ReceiptConfiguration>(_ => new ReceiptConfiguration
        {
            ShopName = "JLek Shop",
            ShopAddress = "",
            ShopPhone = "",
            TaxId = "",
            Footer = "Thank you",
            PaperWidth = 48,
            DefaultPrinter = "null"
        });

        return services;
    }
}