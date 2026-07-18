using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Tables;

namespace JLek.POS.Application.Features.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDiningTableRepository _tableRepository;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IDiningTableRepository tableRepository)
    {
        _orderRepository = orderRepository;
        _tableRepository = tableRepository;
    }

    public async Task<Order> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        // 1. Load DiningTable
        var table = await _tableRepository.GetByIdAsync(
            command.TableId,
            cancellationToken);

        if (table is null)
        {
            throw new InvalidOperationException("Dining table not found.");
        }

        // 2. Create session ID
        var sessionId = OrderSessionId.New();

        // 3. Create Order aggregate in memory (NOT yet persisted)
        var order = Order.Create(
            command.TableId,
            sessionId);

        // 4. Assign table to session (still in memory, NOT yet persisted)
        table.Assign(sessionId);

        // 5. Persist Order first
        await _orderRepository.AddAsync(
            order,
            cancellationToken);

        // 6. Persist Table
        await _tableRepository.UpdateAsync(
            table,
            cancellationToken);

        // 7. Return Order
        return order;
    }
}