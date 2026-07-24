namespace JLek.POS.Web.Presentation.Formatting;

public static class CurrencyFormatter
{
    public static string FormatPrice(decimal price) => $"฿{price:N0}";
}