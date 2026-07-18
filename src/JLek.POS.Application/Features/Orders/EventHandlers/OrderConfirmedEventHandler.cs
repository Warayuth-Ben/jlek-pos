using JLek.POS.Application.Abstractions.EventHandling;
using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Kitchen;
using JLek.POS.Domain.Orders.Events;

namespace JLek.POS.Application.Features.Orders.EventHandlers;

public sealed class OrderConfirmedEventHandler
    : IDomainEventHandler<OrderConfirmedEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IKitchenTicketRepository _kitchenRepository;
    private readonly IProductRepository _productRepository;

    public OrderConfirmedEventHandler(
        IOrderRepository orderRepository,
        IKitchenTicketRepository kitchenRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _kitchenRepository = kitchenRepository;
        _productRepository = productRepository;
    }

    public async Task Handle(
        OrderConfirmedEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        // 1. Load Order to get its items
        var order = await _orderRepository.GetByIdAsync(
            domainEvent.OrderId,
            cancellationToken);

        if (order is null || order.Items.Count == 0)
        {
            return;
        }

        // 2. Load order items with product names
        var firstItem = order.Items.First();
        var firstProduct = await _productRepository.GetByIdAsync(
            JLek.POS.Domain.Catalog.ProductId.From(firstItem.MenuItemId),
            cancellationToken);

        var ticketNumber = domainEvent.OccurredOnUtc.Ticks % 10000;
        var firstItemName = firstProduct?.Name ?? firstItem.MenuItemId.ToString();

        // 3. Create KitchenTicket with first item
        var ticket = KitchenTicket.Create(
            (int)ticketNumber,
            firstItemName,
            firstItem.Quantity.Value,
            null);

        // 4. Add remaining items
        foreach (var item in order.Items.Skip(1))
        {
            var product = await _productRepository.GetByIdAsync(
                JLek.POS.Domain.Catalog.ProductId.From(item.MenuItemId),
                cancellationToken);

            var itemName = product?.Name ?? item.MenuItemId.ToString();

            ticket.AddItem(
                itemName,
                item.Quantity.Value,
                null);
        }

        // 5. Save
        await _kitchenRepository.AddAsync(
            ticket,
            cancellationToken);
    }
}