namespace ShopApp.Solution;

/// <summary>One item in the shopping cart.</summary>
public class Item
{
    public string Name { get; }
    public double Price { get; }
    public int Quantity { get; }

    public Item(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    /// <summary>Price for the whole quantity of this item.</summary>
    public double LineTotal => Price * Quantity;
}
