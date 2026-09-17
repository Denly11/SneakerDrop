using System.ComponentModel.Design;
using System.Drawing;
bool bezi = true;
List<Color> slot = new List<Color>();//sem budu ukládat barzy, do tech slotů
while (bezi)
{
    Console.WriteLine("Menu\n");
    Console.WriteLine("1. Enter color");
    Console.WriteLine("2. Display saved colors");
    Console.WriteLine("3.Display specific color by slot");
    Console.WriteLine("4. Delete color from slot");
    Console.WriteLine("5. Exit");

    Console.Write("Zvol si moznost: ");
    string input = Console.ReadLine();//tady napíše user hodnotu podle toho co chce dělat
                                      //1-5

    if (!int.TryParse(input, out int choice) || choice < 1 || choice > 5)
    {
        Console.WriteLine("E");
        return;
    }

    switch (choice)//1-5
    {
        case 1://vložit barvu
            Console.Write("\nEnter RGB values");
            Console.WriteLine("Enter Red value (0-255): ");
            if (!int.TryParse(Console.ReadLine(), out int R) || R < 0 || R > 255)
            {//tohle se vykoná vždy ale tělo jen když je výsledek !false(true)
                Console.WriteLine("Neplatné číslo");
                return;
            }
            Console.WriteLine("Enter Green value (0-255): ");
            if (!int.TryParse(Console.ReadLine(), out int G) || G < 0 || G > 255)
            {
                Console.WriteLine("Neplatné číslo");
                return;
            }
            Console.WriteLine("Enter Blue value (0-255): ");
            if (!int.TryParse(Console.ReadLine(), out int B) || B < 0 || B > 255)
            {
                Console.WriteLine("Neplatné číslo");
                return;
            }
            Color color = new Color(R, G, B);
            if(slot.Count < 4)
            {
                slot.Add(color);
            }
            else
            {
                Console.WriteLine("Slots are full.");
            }
            Console.WriteLine("\nColor saved.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            break;
        case 2:
            Console.WriteLine($"{slot[0]}, {slot[1]}, {slot[2]}");//zobrazit uložený barvy
            break;
        case 3://zobrazit konkrétní barvu
            break;
        case 4://smazat barvu
            break;
        case 5: bezi = false; //exit, ukončí while cyklus
            Console.WriteLine("Exiting...");
            break;
        default:
            Console.WriteLine("Invalidní hodnota");
            break;
    }
    Console.Clear();
}

    

public class Color
{
    private int _r;
    private int _g;
    private int _b;

    //v set validuje, v get vracím
    public int R {
        get => _r;
        set
        { 
            if (value < 0 || value > 255)
                throw new ArgumentOutOfRangeException(nameof(value), "Hodnota musí být mezi 0 a 255.");
            _r = value;
        }
    }
    public int G {
        get => _g;
        set {
            if (value < 0 || value > 255)
                throw new ArgumentOutOfRangeException(nameof(value), "Hodnota musí být mezi 0 a 255.");
            _g = value;
        }
    }
    public int B {
        get => _b;
        set {
            if (value < 0 || value > 255)
                throw new ArgumentOutOfRangeException(nameof(value), "Hodnota musí být mezi 0 a 255.");
            _b = value;
        }
    }

    public Color(int  _r, int _g, int _b)//<-parametry, sem se v praxi budou dávat argumenty
    {
        R = R;
        G = G;
        B = B;
    }
}