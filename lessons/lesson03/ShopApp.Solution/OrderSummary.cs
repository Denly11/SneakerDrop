namespace ShopApp.Solution;

/// <summary>Result of the order calculation (no printing, just numbers).</summary>
public class OrderSummary
{
    public double Subtotal { get; }
    public double Discount { get; }
    public double Vat { get; }
    public double Total { get; }

    public OrderSummary(double subtotal, double discount, double vat, double total)
    {
        Subtotal = subtotal;
        Discount = discount;
        Vat = vat;
        Total = total;
    }
}
