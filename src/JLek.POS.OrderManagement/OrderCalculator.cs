using System;

namespace JLek.POS.OrderManagement
{
    public class OrderCalculator
    {
        public decimal CalculateTotalPriceWithVAT(decimal price, decimal vatRate)
        {
            return price * (1 + vatRate);
        }
    }
}