using LinqPractice.Solution.Models;

namespace LinqPractice.Solution;

static internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Pokročilé LINQ cvičení ===\n");

        Task1_FilterProducts();
        Task2_FilterStudentsByGrades();
        Task3_SearchBooks();
        Task4_FilterCustomersByOrders();
        Task5_EmployeeStats();

        Console.WriteLine("\nHotovo! Stiskněte libovolnou klávesu pro ukončení.");
        Console.ReadKey();
    }

    // ------------------------------------------------------
    private static void Task1_FilterProducts()
    {
        Console.WriteLine("\n--- Úkol 1: Filtrace produktů ---");

        var products = new List<Product>
        {
            new() { Name = "Notebook", Category = "Elektronika", Price = 25000 },
            new() { Name = "Sluchátka", Category = "Elektronika", Price = 2000 },
            new() { Name = "Kniha", Category = "Knihy", Price = 400 },
            new() { Name = "Myš", Category = "Elektronika", Price = 800 },
            new() { Name = "Mikrovlnka", Category = "Domácnost", Price = 3500 },
        };

        // TODO: Vypište názvy produktů z kategorie "Elektronika" s cenou 1000–5000 seřazené podle ceny sestupně
        // Hint: Where, OrderByDescending, Select

        List<string> result = products
            .Where(p => p.Category == "Elektronika" && p.Price >= 1000 && p.Price <= 5000)
            .OrderByDescending(p => p.Price)
            .Select(p => p.Name)
            .ToList();

        // ------

        foreach (var name in result)
        {
            Console.WriteLine(name);
        }
    }

    // ------------------------------------------------------
    private static void Task2_FilterStudentsByGrades()
    {
        Console.WriteLine("\n--- Úkol 2: Filtrování studentů podle známek ---");

        var students = new List<Student>
        {
            new() { Name = "Adam", Grades = [1, 2, 3] },
            new() { Name = "Eva", Grades = [2, 2, 2] },
            new() { Name = "Tomáš", Grades = [1, 5, 3] },
            new() { Name = "Lenka", Grades = [4, 3, 2] },
        };

        // TODO: Vypište studenty, kteří mají alespoň jednu známku < 3 a žádnou 5
        // Hint: Where, Any, All

        List<Student> result = students
            .Where(s => s.Grades.Any(g => g < 3) && s.Grades.All(g => g != 5))
            .ToList();

        // ------

        foreach (var student in result)
        {
            Console.WriteLine(student.Name);
        }
    }

    // ------------------------------------------------------
    private static void Task3_SearchBooks()
    {
        Console.WriteLine("\n--- Úkol 3: Vyhledávání knih ---");

        var books = new List<Book>
        {
            new() { Title = "C# základy", Author = "Novák", Price = 450 },
            new() { Title = "LINQ v praxi", Author = "Dvořák", Price = 520 },
            new() { Title = "ASP.NET Core v praxi", Author = "Černý", Price = 600 },
            new() { Title = "Programování v C# v praxi", Author = "Bílý", Price = 480 },
        };

        Console.Write("Zadej hledaný výraz: ");
        var query = Console.ReadLine()?.ToLower() ?? "";

        // TODO: Vyhledejte knihy, jejichž Title obsahuje hledaný výraz seřaďte je podle Title vzestupně. Zobraz nejvýše dvě.
        // Hint: Where, Contains, OrderBy, Take

        // query je už převedený na malá písmena, proto porovnáváme i Title v malých písmenech
        List<Book> result = books
            .Where(b => b.Title.ToLower().Contains(query))
            .OrderBy(b => b.Title)
            .Take(2)
            .ToList();

        // ------

        foreach (var book in result)
        {
            Console.WriteLine(book.Title);
        }
    }

    // ------------------------------------------------------
    private static void Task4_FilterCustomersByOrders()
    {
        Console.WriteLine("\n--- Úkol 4: Filtrování zákazníků podle objednávek ---");

        var customers = new List<Customer>
        {
            new()
            {
                Name = "Adam",
                Orders =
                [
                    new() { OrderName = "Notebook", TotalPrice = 25000 },
                    new() { OrderName = "Myš", TotalPrice = 800 }
                ]
            },
            new()
            {
                Name = "Eva",
                Orders =
                [
                    new() { OrderName = "Sluchátka", TotalPrice = 2000 },
                    new() { OrderName = "Klávesnice", TotalPrice = 1500 }
                ]
            },
            new()
            {
                Name = "Tomáš",
                Orders =
                [
                    new() { OrderName = "TV", TotalPrice = 12000 },
                    new() { OrderName = "Mobil", TotalPrice = 9000 }
                ]
            },
            new()
            {
                Name = "Lenka",
                Orders =
                [
                    new() { OrderName = "Fén", TotalPrice = 700 },
                    new() { OrderName = "Žehlička", TotalPrice = 1300 }
                ]
            },
        };

        // TODO: Vypište zákazníky, kteří mají alespoň jednu objednávku nad 5000 Kč a zároveň nemají
        // žádnou objednávku levnější než 1000 Kč. Vypište jejich jména a celkový počet objednávek.
        // Hint: Where, Any, All, Count, Select

        List<(string Name, int OrderCount)> result = customers
            .Where(c => c.Orders.Any(o => o.TotalPrice > 5000) && c.Orders.All(o => o.TotalPrice >= 1000))
            .Select(c => (c.Name, c.Orders.Count))
            .ToList();

        // ------

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Name} : {item.OrderCount}");
        }
    }

    // ------------------------------------------------------
    private static void Task5_EmployeeStats()
    {
        Console.WriteLine("\n--- Úkol 5: Statistiky zaměstnanců ---");

        var employees = new List<Employee>
        {
            new() { Name = "Adam", Department = "IT", Salary = 60000 },
            new() { Name = "Eva", Department = "Finance", Salary = 55000 },
            new() { Name = "Tomáš", Department = "IT", Salary = 72000 },
            new() { Name = "Lenka", Department = "HR", Salary = 45000 },
            new() { Name = "Petr", Department = "IT", Salary = 80000 },
        };

        // TODO:
        // 1) spočítej průměrný plat
        // 2) najdi 3 nejlépe placené
        // 3) spočítej kolik jich má plat nad průměr
        // Hint: Average, OrderByDescending, Take, Count

        double averageSalary = employees.Average(e => e.Salary);
        Console.WriteLine($"Průměrný plat: {averageSalary}");

        List<Employee> bestPaidEmployees = employees
            .OrderByDescending(e => e.Salary)
            .Take(3)
            .ToList();
        Console.WriteLine("3 nejlépe placení zaměstnanci:");
        foreach (var emp in bestPaidEmployees)
        {
            Console.WriteLine($"{emp.Name} - {emp.Salary}");
        }

        int countAboveAverage = employees.Count(e => e.Salary > averageSalary);
        Console.WriteLine($"Počet zaměstnanců s platem nad průměr: {countAboveAverage}");
    }
}
