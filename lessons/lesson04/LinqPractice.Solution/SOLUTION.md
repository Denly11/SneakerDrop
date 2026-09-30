# Lekce 04 – Vzorové řešení (LinqPractice.Solution)

Projekt **LinqPractice.Solution** obsahuje vyřešené úkoly z projektu **LinqPractice**.
Modely (`Models/`) jsou beze změny, doplněný je jen kód v `Program.cs` mezi `TODO` a `// ------`.

> Řešení si prohlédněte **až poté**, co si úkoly sami vyzkoušíte. Stejného výsledku
> lze často dosáhnout i jinou kombinací LINQ metod.

## Úkol 1 – Filtrace produktů

```csharp
products
    .Where(p => p.Category == "Elektronika" && p.Price >= 1000 && p.Price <= 5000)
    .OrderByDescending(p => p.Price)
    .Select(p => p.Name)
    .ToList();
```

- `Where` – obě podmínky (kategorie i cenové rozmezí) v jednom predikátu.
- `OrderByDescending` – řadí se **před** `Select`, dokud máme k dispozici `Price`.
- `Select` – z produktu vezmeme jen název.

## Úkol 2 – Studenti podle známek

```csharp
students
    .Where(s => s.Grades.Any(g => g < 3) && s.Grades.All(g => g != 5))
    .ToList();
```

- `Any` – „alespoň jedna" známka splňuje podmínku.
- `All` – „všechny" známky splňují podmínku → „žádná není 5".

## Úkol 3 – Vyhledávání knih

```csharp
books
    .Where(b => b.Title.ToLower().Contains(query))
    .OrderBy(b => b.Title)
    .Take(2)
    .ToList();
```

- `query` je už převedený na malá písmena, proto i `Title` porovnáváme přes `ToLower()`
  – jinak by hledání bylo citlivé na velikost písmen.
- `Take(2)` až **po** seřazení – jinak bychom vzali první dvě knihy ze zdrojového seznamu.

## Úkol 4 – Zákazníci podle objednávek

```csharp
customers
    .Where(c => c.Orders.Any(o => o.TotalPrice > 5000) && c.Orders.All(o => o.TotalPrice >= 1000))
    .Select(c => (c.Name, c.Orders.Count))
    .ToList();
```

- „Nemají žádnou objednávku levnější než 1000" = „všechny objednávky jsou ≥ 1000" → `All`.
- `Select` vytvoří tuple `(Name, OrderCount)`.

## Úkol 5 – Statistiky zaměstnanců

```csharp
double averageSalary = employees.Average(e => e.Salary);

List<Employee> bestPaidEmployees = employees
    .OrderByDescending(e => e.Salary)
    .Take(3)
    .ToList();

int countAboveAverage = employees.Count(e => e.Salary > averageSalary);
```

- Průměr spočítáme **jednou** a pak ho použijeme v `Count` – nepočítáme ho znovu pro každého zaměstnance.

## Kontrola

Výstup pro hledaný výraz `v praxi`:

```
--- Úkol 1: Filtrace produktů ---
Sluchátka

--- Úkol 2: Filtrování studentů podle známek ---
Adam
Eva
Lenka

--- Úkol 3: Vyhledávání knih ---
Zadej hledaný výraz: v praxi
ASP.NET Core v praxi
LINQ v praxi

--- Úkol 4: Filtrování zákazníků podle objednávek ---
Tomáš : 2

--- Úkol 5: Statistiky zaměstnanců ---
Průměrný plat: 62400
3 nejlépe placení zaměstnanci:
Petr - 80000
Tomáš - 72000
Adam - 60000
Počet zaměstnanců s platem nad průměr: 2
```
