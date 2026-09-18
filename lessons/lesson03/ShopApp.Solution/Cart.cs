namespace ShopApp.Solution;

/// <summary>Shopping cart. Holds items and calculates the order total.</summary>
public class Cart
{
    private const double VatRate = 0.21;

    // Discount tiers: from the given subtotal a discount rate applies.
    private const double SmallOrderThreshold = 1000;
    private const double MediumOrderThreshold = 5000;
    private const double LargeOrderThreshold = 10000;

    private const double SmallOrderDiscount = 0.1;
    private const double MediumOrderDiscount = 0.15;
    private const double LargeOrderDiscount = 0.2;

    private readonly List<Item> _items = new();

    public IReadOnlyList<Item> Items => _items;

    public void Add(string name, double price, int quantity)
    {
        _items.Add(new Item(name, price, quantity));
    }

    /// <summary>Calculates the whole order in one place (subtotal, discount, VAT, total).</summary>
    public OrderSummary Summarize()
    {
        double subtotal = 0;
        foreach (Item item in _items)
        {
            subtotal += item.LineTotal;
        }

        double discount = subtotal * DiscountRate(subtotal);
        double baseAmount = subtotal - discount;
        double vat = baseAmount * VatRate;
        double total = baseAmount + vat;

        return new OrderSummary(subtotal, discount, vat, total);
    }

    private static double DiscountRate(double subtotal)
    {
        if (subtotal > LargeOrderThreshold) return LargeOrderDiscount;
        if (subtotal > MediumOrderThreshold) return MediumOrderDiscount;
        if (subtotal > SmallOrderThreshold) return SmallOrderDiscount;
        return 0;
    }
}
