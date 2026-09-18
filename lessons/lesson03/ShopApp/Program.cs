using ShopApp;

Console.WriteLine("=== SHOP ===");

var c = new Cart();

c.Add("Keyboard", 599, 2);
c.Add("Mouse", 299, 1);
c.Add("Monitor", 4599, 1);
c.Add("USB cable", 89, 5);

c.CalcAndPrint();

Console.WriteLine();
Console.WriteLine("VAT only from the order is: " + c.VatTotal());
