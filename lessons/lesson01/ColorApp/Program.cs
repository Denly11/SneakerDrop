using System.ComponentModel.Design;
using System.Drawing;
bool bezi = true;
int maxSlots = 3;
//List<Color> slot = new List<Color>(maxSlots);//sem budu ukládat barzy, do tech slotů
Color?[] slot = new Color?[maxSlots];

static int  AskForValueInRange(string prompt, int min, int max)
{
    while (true)
    {
        Console.WriteLine(prompt);
        if (!int.TryParse(Console.ReadLine(), out var value) || value < min || value > max)
        {
            Console.WriteLine("Invalid value.");
        }
        else
        {
            return value;
        }
        
    }
}
static void ClearConsole()
{
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

while (bezi)
{
    Console.WriteLine("Menu\n");
    Console.WriteLine("1. Enter color");
    Console.WriteLine("2. Display saved colors");
    Console.WriteLine("3. Display specific color by slot");
    Console.WriteLine("4. Delete color from slot");
    Console.WriteLine("5. Exit");

    //Console.Write("Choose an option: ");
    //string input = Console.ReadLine();//tady napíše user hodnotu podle toho co chce dělat
                                      //1-5
    //Console.WriteLine("\n");

    //int choice = 0; 
    //while (true)
    //{ 
    //    if (!int.TryParse(input, out int choiceInput) || choiceInput < 1 || choiceInput > 5)
    //    {
    //        Console.WriteLine("Invalid option.");
    //        Console.WriteLine("Press any key to try again...");
    //        Console.ReadKey();
    //        break;
    //    }
    //    else
    //    {
    //        choice = choiceInput;
    //        break;
    //    }
    //}

    int choice = AskForValueInRange("Choose an option (1-5): ", 1, 5);
    switch (choice)//1-5
    {
        case 1://vložit barvu
            Console.Write("\nEnter RGB values");
 
            int R = AskForValueInRange("Enter Red value (0-255): ", 0, 255);
            int G = AskForValueInRange("Enter Green value (0-255): ", 0, 255);
            int B = AskForValueInRange("Enter Blue value (0-255): ", 0, 255);

            //while (true)
            //{
            //    Console.WriteLine("Red value (0-255): ");
            //    if (!int.TryParse(Console.ReadLine(), out R) || R < 0 || R > 255)
            //    {//tohle se vykoná vždy ale tělo jen když je výsledek !false(true)
            //        Console.WriteLine("Neplatné číslo");
            //    }
            //    else
            //    {
            //        break;
            //    }

            //}
            //while (true)
            //{
            //    Console.WriteLine("Green value (0-255): ");
            //    if (!int.TryParse(Console.ReadLine(), out G) || G < 0 || G > 255)
            //    {//tohle se vykoná vždy ale tělo jen když je výsledek !false(true)
            //        Console.WriteLine("Neplatné číslo");
            //    }
            //    else
            //    {
            //        break;
            //    }

            //}
            //while (true)
            //{
            //    Console.WriteLine("Blue value (0-255): ");
            //    if (!int.TryParse(Console.ReadLine(), out B) || B < 0 || B > 255)
            //    {//tohle se vykoná vždy ale tělo jen když je výsledek !false(true)
            //        Console.WriteLine("Neplatné číslo");
            //    }
            //    else
            //    {
            //        break;
            //    }

            //}
            Color color = new Color(R, G, B);

            //int freeSlotIndex = 0;//aby to neodpovídalo žádnýmu indexu

            //for (int i = 0; i < slot.Length; i++)//tohle prostě projede každý slot po indexech(0-2)
            //{
            //    int 
            //    if (slot[i] != null)//když je slot prázdný
            //    {
            //        slot[i] = color;                                      PROSTĚ KONTROLA ŽE TAM JE NĚJAKÝ MÍSTO
            //        Console.WriteLine("\nColor saved.");
            //        break;
            //    }

            //}
            int freeSlotCount = 0;
            foreach (var item in slot)
            {
                if (item == null)
                {
                    freeSlotCount++;//spočítá volné sloty
                    break;
                }
            }

            if(freeSlotCount == 0)//nejsou velný sloty
            {
                Console.WriteLine("Could not save color, Slots are full!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            }
            else
            { 
                for (int i = 0; i < slot.Length; i++)//tohle prostě projede každý slot po indexech(0-2)
                {
                    if (slot[i] == null)//když je slot prázdný
                    {
                        slot[i] = color;
                        Console.WriteLine("\nColor saved.");
                        break;
                    }
                }
            }
            //Console.WriteLine("Could not save color, Slots are full!");
            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
            ClearConsole();
            break;

        //if (slot.Count < maxSlots)
        //{
        //    slot.Add(color);
        //    Console.WriteLine("\nColor saved.");
        //    Console.WriteLine("Press any key to continue...");
        //    Console.ReadKey();
        //    break;
        //}
        //else
        //{
        //    Console.WriteLine("Could not save color, Slots are full!");
        //    Console.WriteLine("Press any key to continue...");
        //    Console.ReadKey();
        //    break;
        //}
        case 2://vypsat všechny barvy
            for(int i = 0; i < slot.Length; i++)
            {
                if(slot[i] == null)
                {
                    Console.WriteLine($"Slot {i + 1}: [Empty].");
                }
                else
                {
                    string hexColor = slot[i].convertColorToHexadecimal(slot[i].R, slot[i].G, slot[i].B);
                    Console.WriteLine($"Slot {i + 1}: {slot[i]} ---> {hexColor}");
                }
            }
            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
            ClearConsole() ;
            break;


            //if (slot.Count == 0)
            //{
            //    for(int i = 0; i < maxSlots; i++)
            //    {
            //        Console.WriteLine($"Slot {i + 1}: [Empty].");
            //    }
            //    Console.WriteLine("\n");
            //}
            //else
            //{
            //    for (int i = 0; i < slot.Count; i++)
            //    {   // slot[i] <--- použije Color.ToString()
            //        Console.WriteLine($"Slot {i + 1}: {slot[i]}");
            //    }
            //    for (int i = slot.Count; i < maxSlots; i++)
            //    {
            //        Console.WriteLine($"Slot {i + 1}: [Empty]");
            //    }
            //    Console.WriteLine("Press any key to go bach to menu...");
            //    Console.ReadKey();
            //    break;
            //}
            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
            //break;
        case 3://zobrazit konkrétní barvu
            //int numberOfSlotToDiplay = 0;

            //Console.WriteLine("Enter slot number do display(1-3): ");
            int numberOfSlotToDiplay = AskForValueInRange("Enter slot number to display (1-3): ", 1, 3);
            for (int i = 0; i < slot.Length; i++)
            {
                if (slot[i] == null)//je prázdný(empty), tak...
                {
                    Console.WriteLine($"The slot is empty.");
                    break;

                }
                else//pokud v něm něco je
                {
                    string hexColor = slot[numberOfSlotToDiplay - 1].convertColorToHexadecimal(slot[numberOfSlotToDiplay - 1].R, slot[numberOfSlotToDiplay - 1].G, slot[numberOfSlotToDiplay - 1].B);
                    Console.WriteLine($"Slot {numberOfSlotToDiplay}: {slot[numberOfSlotToDiplay - 1]} ---> {hexColor}\n");
                    break;
                }
            }

            //while (true)
            //{
            //    if (!int.TryParse(Console.ReadLine(), out numberOfSlotToDiplay) || numberOfSlotToDiplay < 1 || numberOfSlotToDiplay > 3)
            //    {
            //        Console.WriteLine("Invalid slot number");
            //        break;
            //    }
            //    else
            //    {
            //        for (int i = 0; i < slot.Length; i++)
            //        {
            //            if (slot[i] == null)//je prázdný(empty), tak...
            //            {
            //                Console.WriteLine($"The slot is empty.");
            //                break;

            //            }
            //            else//pokud v něm něco je
            //            {

            //                string hexColor = slot[numberOfSlotToDiplay - 1].convertColorToHexadecimal(slot[numberOfSlotToDiplay - 1].R, slot[numberOfSlotToDiplay - 1].G, slot[numberOfSlotToDiplay - 1].B);
            //                Console.WriteLine($"Slot {numberOfSlotToDiplay}: {slot[numberOfSlotToDiplay - 1]} ---> {hexColor}\n");
            //                break;
            //            }
            //        }
            //    }
            //}

            //Console.WriteLine("Press any key to go back to menu...");
            //Console.ReadKey();
            ClearConsole();
            break;
        case 4:
            //int numberOfSlotToDelete = 0;
            int numberOfSlotToDelete = AskForValueInRange("Enter slot number to delete (1-3): ", 1, 3);
            if (slot[numberOfSlotToDelete - 1] == null)
            {
                Console.WriteLine("Slot is empty.");
            }
            else
            {
                slot[numberOfSlotToDelete - 1] = null;//přepíše na prázdný
                Console.WriteLine($"Slot {numberOfSlotToDelete} deleted.");
            }
            //while (true)
            //{
            //    Console.WriteLine("Choose slot to delete (1-3): ");
            //    if (!int.TryParse(Console.ReadLine(), out numberOfSlotToDelete) || numberOfSlotToDelete < 1 || numberOfSlotToDelete > 3)
            //    {
            //        Console.WriteLine("Invalid slot number");
            //        break;
            //    }
            //    else if(slot[numberOfSlotToDelete - 1] == null)
            //    { 
            //        Console.WriteLine("Slot is empty.");
            //        break;
            //    }
            //    else
            //    {
            //        slot[numberOfSlotToDelete - 1] = null;//přepíše na prázdný
            //        Console.WriteLine($"Slot {numberOfSlotToDelete} deleted.");
            //        break;
            //    }
            //}
            //Console.WriteLine("Press any key to go back to menu...");
            //Console.ReadKey();
            ClearConsole();
            break;
        case 5: bezi = false; //exit, ukončí while cyklus
            Console.WriteLine("Exiting...");
            break;
        default:
            Console.WriteLine("Invalid value.");
            break;
    }
    Console.Clear();//"vrátí" to program do menu(vzhledově, logicky to je ten while...)
}

    

public class Color
{
    private int _r;
    private int _g;
    private int _b;

    //v set validuju, v get vracím
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
        R = _r;
        G = _g;
        B = _b;
    }

    public override string ToString() => $"RGB({R}, {G}, {B})";
    // slot[i] <--- použije Color.ToString(), tohle je TEN výpis z toho

    public string convertColorToHexadecimal(int R, int G, int B)
    {
        if (R < 0 || R > 255 || G < 0 || G > 255 || B < 0 || B > 255)
            throw new ArgumentOutOfRangeException("Values must be in the range of 0–255.");

        return $"#{R:X2}{G:X2}{B:X2}";// X2 znamená, že se hodnota převede na hexadecimální formát a bude mít vždy 2 znaky
    }
}