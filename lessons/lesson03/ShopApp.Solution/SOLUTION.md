# Lekce 03 – Vzorové řešení (ShopApp.Solution)

Projekt **ShopApp.Solution** je vyčištěná verze aplikace **ShopApp**. Dělá přesně
totéž, ale dodržuje jmenné konvence i návrhové principy z prezentace.

> Řešení si prohlédněte **až poté**, co si refactoring sami vyzkoušíte. Není to
> jediné „správné" řešení – existuje víc čistých variant. Důležité je, aby
> výsledek programu zůstal stejný.

## Struktura řešení

| Soubor | Odpovědnost |
|--------|-------------|
| `Item.cs` | Reprezentace jedné položky (název, cena, počet) + cena za řádek. |
| `OrderSummary.cs` | Výsledek výpočtu objednávky (mezisoučet, sleva, DPH, celkem). |
| `Cart.cs` | Košík – drží položky a **počítá** objednávku. |
| `Program.cs` | Prezentační vrstva – **vypisuje** výsledek. |

## Co se změnilo a proč

### 1. Jmenné konvence
- `cart` → **`Cart`**, `item` → **`Item`** – třídy v PascalCase.
- `add` → **`Add`**, `calcAndPrint`/`vatTotal` → **`Summarize`** – metody v PascalCase.
- Kryptické názvy polí `N`, `c`, `q`, `x`, `disc` → **`Name`, `Price`, `Quantity`,
  `subtotal`, `discount`** – smysluplné, popisné názvy (srozumitelnost > stručnost).
- Privátní pole `_items` – začíná podtržítkem, camelCase.

### 2. Zapouzdření (encapsulation)
- Veřejná pole (`public string N;` …) nahrazena **vlastnostmi** jen pro čtení
  (`Name { get; }`), hodnoty se nastaví v **konstruktoru**.
- Seznam položek je privátní (`_items`), ven se vystavuje jen `IReadOnlyList<Item>`,
  takže ho nikdo zvenčí omylem nerozbije.

### 3. Magická čísla → pojmenované konstanty (`Cart.cs`)
- `0.21` → `VatRate`
- `1000` / `5000` / `10000` → `SmallOrderThreshold` / `MediumOrderThreshold` /
  `LargeOrderThreshold`
- `0.1` / `0.15` / `0.2` → `SmallOrderDiscount` / `MediumOrderDiscount` /
  `LargeOrderDiscount`

Změna sazby DPH nebo pravidel slev se teď dělá **na jednom místě**.

### 4. DRY / WET
- Původně byl výpočet mezisoučtu, slevy a DPH **dvakrát** (`calcAndPrint`
  i `vatTotal`). Nyní existuje **jen jedna** metoda `Summarize()`, která vrátí
  `OrderSummary`. Program si z něj vezme jak celkovou částku, tak samotné DPH
  (`summary.Vat`).

### 5. SoC / SRP – oddělení odpovědností
- `Cart` **jen počítá** a vrací data (`OrderSummary`).
- `Program` **jen vypisuje**. Výpočet a výstup už nejsou v jedné metodě.

### 6. KISS – zjednodušení
- Zbytečně zanořený výpočet slevy s pomocnou proměnnou `hasDiscount`, `== true`
  a prázdnými větvemi `else` byl nahrazen krátkou metodou `DiscountRate()` se
  třemi „guard" podmínkami (od nejvyšší hranice).
- `ItemCount = ItemCount + 1` a indexované `for (int i …; Data[i])` cykly
  nahrazeny čistým `foreach`.

### 7. YAGNI
- Nepoužívaná konstanta `MAX` byla **smazána** – co se nepoužívá, do kódu nepatří.

### 8. Formátování
- Sjednocené odsazení, konzistentní styl.
- Výpis položky používá **interpolaci řetězců** (`$"{item.Name} …"`) místo
  ručního skládání přes `+`.

## Kontrola

Výstup vzorového řešení je identický s původní (rozbitou) aplikací:

```
=== SHOP ===
Items:
Keyboard 599 x 2 = 1198
Mouse 299 x 1 = 299
Monitor 4599 x 1 = 4599
USB cable 89 x 5 = 445
Subtotal: 6541
Discount: 981,15
VAT: 1167,5685
Total: 6727,418500000001

VAT only from the order is: 1167,5685
```
