using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Payments.Responses;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Payments;

namespace JLek.POS.Application.Features.Payments.Commands.ReceivePayment;

public sealed class ReceivePaymentCommandHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;

    public ReceivePaymentCommandHandler(
        IOrderRepository orderRepository,
        IPaymentRepository paymentRepository)
    {
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentResponse> Handle(
        ReceivePaymentCommand command,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(
            command.OrderId,
            cancellationToken);

        if (order is null)
        {
            throw new InvalidOperationException("Order not found.");
        }

        var amountReceived = Money.From(command.AmountReceived);
        var payment = Payment.Create(order, amountReceived, command.Method);

        await _paymentRepository.AddAsync(payment, cancellationToken);

        return PaymentResponse.FromDomain(payment);
    }
}