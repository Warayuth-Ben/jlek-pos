using JLek.POS.Application.Features.Orders.Commands.AddItem;
using JLek.POS.Application.Features.Orders.Commands.CompleteOrder;
using JLek.POS.Application.Features.Orders.Commands.ConfirmOrder;
using JLek.POS.Application.Features.Orders.Commands.CreateOrder;
using JLek.POS.Application.Features.Orders.Queries.GetOrderById;
using Microsoft.Extensions.DependencyInjection;

namespace JLek.POS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<CreateOrderCommandHandler>();
        services.AddScoped<AddItemCommandHandler>();
        services.AddScoped<ConfirmOrderCommandHandler>();
        services.AddScoped<CompleteOrderCommandHandler>();
        services.AddScoped<GetOrderByIdQueryHandler>();

        return services;
    }
}