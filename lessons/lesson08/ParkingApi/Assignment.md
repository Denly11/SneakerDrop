# Zadání: Parking REST API

## O aplikaci

Městská parkovací společnost provozuje několik parkovišť: v centru, u nádraží nebo u letiště. Dosud je evidovala v tabulkách, teď ale chystá web a mobilní aplikaci, ve kterých řidiči uvidí, kolik parkování stojí, kdy má parkoviště otevřeno a jestli tam nabijí elektroauto. Vaším úkolem je naprogramovat backend, ze kterého budou obě aplikace brát data.

Správce společnosti zakládá parkoviště, mění jejich ceny a otevírací dobu a na každém parkovišti postupně vyznačuje jednotlivá parkovací místa, každé se svým označením, např. `B-017`. Některá místa jsou vyhrazená pro elektroauta a mají nabíječku. Každé parkoviště má danou kapacitu, kterou počet vyznačených míst nesmí překročit. Později chce společnost prodávat i permanentky (viz bonusové úkoly).

## Úkol

Naprogramujte REST API pro správu parkovišť a jejich parkovacích míst.

## Technická omezení

- Data se ukládají pouze v paměti.
- Každá entita má vlastní repozitář. Repozitáře se navzájem neznají, nevolají a neobsahují žádnou business logiku.
- Entity se na sebe odkazují jen identifikátorem. Parkoviště nemá kolekci svých míst.
- Business pravidla a kombinování dat z více repozitářů patří výhradně do service vrstvy. Controller zná jen service.
- Request a response modely jsou oddělené od entit.
- Hodnoty spravované serverem (identifikátor, čas vytvoření a úpravy) klient neposílá.

## Business specifikace

### Parkoviště

| Pole | Popis | Omezení |
| --- | --- | --- |
| `id` | Identifikátor | Generuje server. |
| `name` | Název parkoviště | Povinný, 3–100 znaků. |
| `capacity` | Maximální počet míst | Povinná, celé číslo 1–2000. |
| `hourlyRateCzk` | Cena za hodinu v Kč | Povinná, 0,01–500, nejvýše 2 desetinná místa. |
| `hasEvCharging` | Zda parkoviště nabízí nabíjení elektromobilů | Povinné, ano/ne. |
| `openingHours` | Otevírací doba (`open`, `close`) | Nepovinná, nevyplněná znamená nonstop provoz. Je-li vyplněná, jsou `open` i `close` povinné ve formátu `HH:mm` a `close` je později než `open`. |
| `createdAt`, `updatedAt` | Čas vytvoření a poslední úpravy | Nastavuje server. Úprava mění jen `updatedAt`. |
| `freeSpots` | Počet volných míst | Dopočítává server: kapacita minus počet míst parkoviště. |

### Parkovací místo

Každé místo patří právě jednomu parkovišti.

| Pole | Popis | Omezení |
| --- | --- | --- |
| `id` | Identifikátor | Generuje server. |
| `label` | Označení místa | Povinné, formát písmeno, pomlčka, tři číslice (např. `B-017`). Malá písmena se převedou na velká a označení se uloží v této podobě. |
| `type` | Typ místa | Povinný, číslo `0` (`Standard`) nebo `1` (`Electric`). |

### Business pravidla

| Kód | Pravidlo |
| --- | --- |
| `LOT-01` | Název parkoviště je unikátní bez ohledu na velikost písmen. Při úpravě se parkoviště nepočítá samo se sebou. |
| `LOT-02` | Kapacitu parkoviště nelze snížit pod počet jeho existujících míst. |
| `SPOT-01` | Místo lze přidat jen k existujícímu parkovišti. |
| `SPOT-02` | Místo typu `Electric` lze přidat jen na parkoviště s nabíjením. |
| `SPOT-03` | Počet míst parkoviště nesmí překročit jeho kapacitu. |

## Úkoly

Endpointy (HTTP metody a cesty) navrhněte sami podle konvencí REST.

1. **Vytvoření parkoviště** – platí `LOT-01`.
2. **Detail parkoviště** – odpověď obsahuje i `freeSpots`.
3. **Úprava parkoviště** – plná náhrada všech polí. Platí `LOT-01` a `LOT-02`.
4. **Přidání parkovacího místa** – platí `SPOT-01` až `SPOT-03`.

## Bonusové úkoly: Permanentky

Společnost chce kromě hodinového parkování prodávat i permanentky. Permanentka opravňuje jedno vozidlo parkovat na jednom parkovišti po dobu své platnosti. Rozšiřte API o vydávání a vyhledávání permanentek jako nového samostatného zdroje. I pro permanentky platí všechna technická omezení výše; na parkoviště se permanentka odkazuje jen identifikátorem.

### Permanentka

| Pole | Popis | Omezení |
| --- | --- | --- |
| `id` | Identifikátor | Generuje server. |
| `parkingLotId` | Parkoviště, pro které permanentka platí | Povinné. |
| `licensePlate` | SPZ vozidla | Povinná. Odstraní se mezery a malá písmena se převedou na velká; výsledek má 5–8 znaků a uloží se v této podobě. |
| `holderEmail` | E-mail držitele | Povinný, platná e-mailová adresa. |
| `validFrom` | První den platnosti (datum bez času) | Povinný, nejdříve dnes a nejpozději 3 měsíce od dneška. |
| `durationMonths` | Délka platnosti v měsících | Povinná, pouze 1, 3, 6 nebo 12. |
| `requiresCharging` | Zda držitel potřebuje nabíjení elektromobilu | Povinné, ano/ne. |
| `validTo` | Poslední den platnosti | Dopočítává server: `validFrom` plus `durationMonths` měsíců minus jeden den. |
| `priceCzk` | Cena permanentky | Dopočítává server: hodinová sazba parkoviště × 100 × počet měsíců, u 12 měsíců se sníží o 10 %; zaokrouhlí se na celé koruny. Cena se při pozdější změně sazby parkoviště nemění. |
| `createdAt` | Čas vytvoření | Nastavuje server. |

### Business pravidla

| Kód | Pravidlo |
| --- | --- |
| `PASS-01` | Permanentka musí odkazovat na existující parkoviště. |
| `PASS-02` | Stejná SPZ nesmí mít na stejném parkovišti dvě permanentky s překrývající se platností. |
| `PASS-03` | Permanentku s požadavkem na nabíjení lze vydat jen pro parkoviště s nabíjením. |

### Úkoly

- **B1: Vydání permanentky** – platí `PASS-01` až `PASS-03`; odpověď obsahuje dopočítané `validTo` a `priceCzk`.
- **B2: Seznam permanentek** – s nepovinnými filtry `parkingLotId`, `licensePlate` a `validOn` (datum). Filtr `licensePlate` se normalizuje stejně jako při vydání, `validOn` vrací permanentky platné v daný den. Výsledek je seřazený podle `validFrom`.
