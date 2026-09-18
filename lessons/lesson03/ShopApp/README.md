# Lekce 03 – Clean Code: najdi a oprav chyby

Ukázková konzolová aplikace **ShopApp** je napsaná **záměrně špatně**. Funguje
(spočítá a vypíše objednávku v e-shopu), ale porušuje řadu zásad z prezentace
o čistém kódu.

Vaším úkolem je kód **zrefaktorovat** tak, aby dělal přesně to samé, ale byl
čistý, čitelný a dodržoval jmenné konvence i návrhové principy.

> Kód i názvy pište **anglicky** (třídy, metody, proměnné, výpisy).

## Výstup aplikace

Očekávaný výstup (musí zůstat stejný i po refactoringu):

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

## Zadání

1. **Přečtěte si celý kód** tohoto projektu.
2. **Najděte** co nejvíce prohřešků proti Clean Code.
3. **Opravte je** – čistěji pojmenujte, rozdělte odpovědnosti, odstraňte
   duplikace, zjednodušte.
4. **Ověřte**, že program po úpravách vypisuje stále stejný výsledek.

> ⚠️ Chování programu se nesmí změnit. Refactoring = mění se struktura a čitelnost
> kódu, **ne** jeho výsledek.

## Podle čeho kód posuzovat

Projděte kód a u každé části se ptejte, jestli dodržuje zásady z prezentace:

- **Jmenné konvence** – PascalCase, camelCase, prefix `_` u privátních polí,
  smysluplné a popisné názvy (srozumitelnost > stručnost), žádné zkratky ani
  jednopísmenné názvy.
- **Zapouzdření** – co má a nemá být přístupné zvenčí.
- **Magická čísla** – jsou v kódu „záhadné" konstanty bez jména?
- **DRY** – neopakuje se stejná logika na více místech?
- **SoC / SRP** – dělá každá třída a metoda právě jednu věc?
- **KISS** – nešlo by to napsat jednodušeji?
- **YAGNI** – není v kódu něco navíc, co se vůbec nepoužívá?
- **POLA** – dělá metoda opravdu to, co slibuje její název?
- **Formátování** – odsazení, konzistentní styl, mezery.

Tip: přejmenování zvládne IDE bezpečně a najednou přes *Rename* (CTRL + R). Po každé
úpravě spusťte aplikaci a ověřte, že výstup pořád sedí.

## Vzorové řešení

Vyčištěná verze je v projektu **`ShopApp.Solution`** a komentář ke každé opravě
najdete v [SOLUTION.md](../ShopApp.Solution/SOLUTION.md). Podívejte se na něj
**až poté**, co si refactoring sami vyzkoušíte.
