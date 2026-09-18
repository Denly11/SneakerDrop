using ShopApp.Solution;

Console.WriteLine("=== SHOP ===");

var cart = new Cart();
cart.Add("Keyboard", 599, 2);
cart.Add("Mouse", 299, 1);
cart.Add("Monitor", 4599, 1);
cart.Add("USB cable", 89, 5);

OrderSummary summary = cart.Summarize();

Console.WriteLine("Items:");
foreach (Item item in cart.Items)
{
    Console.WriteLine($"{item.Name} {item.Price} x {item.Quantity} = {item.LineTotal}");
}

Console.WriteLine("Subtotal: " + summary.Subtotal);
Console.WriteLine("Discount: " + summary.Discount);
Console.WriteLine("VAT: " + summary.Vat);
Console.WriteLine("Total: " + summary.Total);

Console.WriteLine();
Console.WriteLine("VAT only from the order is: " + summary.Vat);
