namespace ShopApp;

public class Cart
{
    private List<Item> _data = new List<Item>();
    int ItemCount = 0;

    public void Add(string n, double c, int q)
    {
        Item p = new Item();
        p.n = n;
        p.c = c;
        p.q = q;
        _data.Add(p);
        ItemCount = ItemCount + 1;
    }

    public void CalcAndPrint()
    {
        double x = 0;
        for (int i = 0; i < _data.Count; i++)
        {
            x = x + _data[i].c * _data[i].q;
        }

        double disc = 0;
        double rate = 0;
        bool hasDiscount = false;
        if (x > 1000)
        {
            hasDiscount = true;
            if (x > 5000)
            {
                if (x > 10000)
                {
                    rate = 0.2;
                }
                else
                {
                    rate = 0.15;
                }
            }
            else
            {
                rate = 0.1;
            }
        }
        else
        {
            hasDiscount = false;
        }
        if (hasDiscount == true)
        {
            disc = x * rate;
        }
        else
        {
            disc = 0;
        }

        double baseAmount = x - disc;
        double vat = baseAmount * 0.21;
        double total = baseAmount + vat;

        Console.WriteLine("Items:");
        for (int i = 0; i < _data.Count; i++)
        {
            Console.WriteLine(_data[i].n + " " + _data[i].c + " x " + _data[i].q + " = " + (_data[i].c * _data[i].q));
        }

        Console.WriteLine("Subtotal: " + x);
        Console.WriteLine("Discount: " + disc);
        Console.WriteLine("VAT: " + vat);
        Console.WriteLine("Total: " + total);
    }

    // Returns the VAT amount from the whole order
    public double VatTotal()
    {
        double x = 0;
        for (int i = 0; i < _data.Count; i++)
        {
            x = x + _data[i].c * _data[i].q;
        }

        double disc = 0;
        double rate = 0;
        bool hasDiscount = false;
        if (x > 1000)
        {
            hasDiscount = true;
            if (x > 5000)
            {
                if (x > 10000)
                {
                    rate = 0.2;
                }
                else
                {
                    rate = 0.15;
                }
            }
            else
            {
                rate = 0.1;
            }
        }
        else
        {
            hasDiscount = false;
        }
        if (hasDiscount == true)
        {
            disc = x * rate;
        }
        else
        {
            disc = 0;
        }

        double baseAmount = x - disc;
        return baseAmount * 0.21;
    }
}
