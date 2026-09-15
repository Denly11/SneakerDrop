using System.ComponentModel.Design;
using System.Drawing;

Console.WriteLine("Menu");

Console.Write("Zvol si moznost: ");
string input = Console.ReadLine();
//1-5

if(!int.TryParse(input, out int choice) || choice <1 ||choice > 5)
{
    Console.WriteLine("E");
    return;
}

switch (choice)//1-5
{
    case 1:
        Color color = new Color();
        color = 
        break;
    case 2:
        break;
    case 3:
        break;
    default: Console.WriteLine("Invalidní hodnota");
        break;
}
    

public class Color
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
}