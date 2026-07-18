namespace JLek.POS.Web.Contracts.Reports;

public sealed record DailySalesReport(
    string Date,
    int TotalOrders,
    decimal TotalRevenue,
    decimal TotalRefunds,
    decimal NetRevenue,
    decimal AverageOrderValue,
    int TotalItemsSold);

public sealed record SalesByPaymentReport(
    string DateFrom,
    string DateTo,
    IReadOnlyList<PaymentMethodSummary> PaymentMethods);

public sealed record PaymentMethodSummary(
    string Method,
    int TransactionCount,
    decimal TotalAmount);

public sealed record BestSellerReport(
    string DateFrom,
    string DateTo,
    int Limit,
    IReadOnlyList<BestSellerItem> Items);

public sealed record BestSellerItem(
    int Rank,
    Guid MenuItemId,
    int TotalQuantity,
    decimal TotalRevenue,
    int OrderCount);