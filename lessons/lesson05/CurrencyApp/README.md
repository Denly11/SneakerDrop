# Currency App (WPF)

Vytvořte **WPF aplikaci** pro práci s kurzy měn, které každý pracovní den
vyhlašuje Česká národní banka (ČNB).

Projekt **CurrencyApp** obsahuje hotové UI a prázdné metody na obsluhu událostí.
Můžete využít již připravené třídy v složkách Models a Services.ok
Třídy si můžete upravit, přidat nové, nebo si vytvořit vlastní.

> Kód i názvy pište **anglicky**.

Zadání má dvě části, obě ve **stejném projektu**:

| Část |
|------|
| **1 – Základní verze** |
| **2 – Rozšířená verze** |

Ovládací prvky části 2 jsou v okně vidět od začátku – v části 1 si jich
nevšímejte.

## Vstupní data

Denní kurzovní lístek ČNB ve formátu XML:

<https://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.xml>

Kurzy za jiný den získáte parametrem `date`, např.
`...denni_kurz.xml?date=24.09.2026`.

Dokument si otevřete a prohlédněte – pozor na formát čísel a na význam
atributu `mnozstvi`.

---

## Část 1 – Základní verze

1. Stáhněte si XML lokálně (klidně víc souborů za různé dny) do složky `Data` v rámci CurrencyApp.
2. Načtěte XML do objektů a zobrazte kurzy v UI.
3. Stažená data ukládejte ve formátu JSON do **jednoho** souboru.
4. Podporujte ukládání kurzů z různých dnů – pro jeden den lze uložit **jen
   jeden** seznam (pro datum použijte `DateTime`).
5. Umožněte procházet měny napříč uloženými dny.

---

## Část 2 – Rozšířená verze

XML se už nestahuje ručně – aplikace si ho stáhne sama.

1. **Stahování** – XML stahujte pomocí `HttpClient` pro datum zvolené v UI.
   UI během stahování nesmí zamrznout, chyby sítě nesmí shodit aplikaci.
2. **Deserializace** – XML deserializujte do vlastních tříd pomocí
   `XmlSerializer` a dál pracujte s nimi.
3. **`DateTime`**
   - Procházení po pracovních dnech (◀ / ▶), návrat na dnešek.
   - Pokud ČNB vrátí lístek k jinému datu, než jste chtěli, upozorněte na to.
   - Evidujte čas stažení a zobrazte, jak staré kurzy jsou.
4. **`Guid`** – každý uložený lístek má jedinečný identifikátor, podle kterého
   ho lze smazat.
5. **`Random`**
   - Stažení kurzů za náhodný pracovní den v posledním roce.
   - Převodník měn na CZK; tlačítkem 🎲 vybere náhodnou měnu a částku.
6. **Filtr** – vyhledávání v tabulce kurzů.
