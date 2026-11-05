# SneakerDrop

## Specifikace

### O aplikaci

Obchod vypisuje **dropy**, tedy limitované edice tenisek s omezeným počtem kusů a pevným datem startu. Po startu dropu si na něj sběratelé vytvářejí **rezervace** na konkrétní velikost. Jakmile jsou všechny kusy zarezervované, drop je vyprodaný. Dropy spravuje jen administrátor, rezervace může vytvářet kdokoliv.

### Funkce aplikace

S dropy umí aplikace tyto operace:

- zobrazit seznam všech dropů,
- zobrazit detail dropu,
- vytvořit, upravit a smazat drop (jen administrátor).

S rezervacemi umí aplikace tyto operace:

- zobrazit všechny rezervace daného dropu,
- vytvořit rezervaci na daný drop,
- zobrazit detail rezervace,
- upravit rezervaci,
- zrušit rezervaci.

Administrátor se prokazuje hlavičkou `X-Api-Key: sneaker-admin`. Pokud hlavička u administrátorské operace chybí nebo má špatnou hodnotu, požadavek skončí chybou.

### Technické požadavky

- ASP.NET Core Web API s controllery.
- Dva controllery: jeden pro dropy, druhý pro rezervace.
- Výpis rezervací dropu a vytvoření rezervace pracují s rezervacemi jako s **podzdrojem dropu**.
- Detail, úprava a zrušení rezervace probíhají přes **controller pro rezervace**.
- Dva repozitáře. Data se drží v paměti, v listu uvnitř repozitáře.
- Jedna služba s business logikou.
- Entity jsou oddělené od Request a Response DTO. Entita se nikdy nevrací přímo z API.
- Vstupní data se ověřují **validačními atributy**, business pravidla se ověřují **ve službě**.
- Každá odpověď vrací vhodný HTTP stavový kód.

Endpointy, strukturu projektu i názvy tříd navrhujete sami.

### Drop

| Údaj | Omezení |
|---|---|
| název | povinný, 3–50 znaků |
| značka | povinná |
| popisek | nepovinný, nejvýše 200 znaků |
| cena | povinná, 1–100 000 Kč |
| počet kusů | povinný, 0–1000 |
| datum startu | povinné; u nového dropu musí být v budoucnosti |

### Rezervace

| Údaj | Omezení |
|---|---|
| jméno zákazníka | povinné, 2–50 znaků |
| e-mail | povinný, platný formát |
| velikost | povinná, 36–48 |
| datum vytvoření | nastavuje systém |

### Business pravidla

1. Rezervovat lze jen drop, který už začal.
2. Počet rezervací nesmí překročit počet kusů dropu.
3. Jeden e-mail smí mít na jeden drop nejvýše jednu rezervaci.
4. Počet kusů dropu nelze snížit pod počet existujících rezervací.
5. Drop, který má rezervace, nelze smazat.
6. E-mail zákazníka se nikdy nevrací v odpovědi API.

### Ukázka dat

> Může se lišit dle implementace, ale musí obsahovat všechny údaje a dodržovat omezení.

**Drop – vstup:**

```json
{ "name": "Air Comet Retro", "brand": "Nike", "description": "Retro model z roku 1996", "price": 4290, "stock": 3, "releaseDate": "2026-12-01T18:00:00Z" }
```

**Drop – výstup:**

```json
{ "id": 1, "name": "Air Comet Retro", "brand": "Nike", "description": "Retro model z roku 1996", "price": 4290, "stock": 3, "availableCount": 2, "releaseDate": "2026-12-01T18:00:00Z" }
```

**Rezervace – vstup:**

```json
{ "customerName": "Hana Nováková", "email": "hana@skola.cz", "size": 39 }
```

**Rezervace – výstup:**

```json
{ "id": 1, "dropId": 1, "customerName": "Hana Nováková", "size": 39, "createdAt": "2026-10-22T09:15:00Z" }
```

**Chyba porušeného business pravidla:**

```json
{ "message": "Drop je vyprodaný." }
```

### Doporučená výchozí data

Pro snadné testování se doporučuje, aby aplikace po spuštění obsahovala dva dropy:

- jeden, který **už probíhá** a má jen **3 kusy**, abyste rychle otestovali vyprodání,
- jeden, který **začne až v budoucnosti**.

---

## Část 2: Nápovědy

### 1. Endpointy

**DropsController**

| Metoda | URL | Operace |
|---|---|---|
| GET | `/api/drops` | seznam dropů |
| GET | `/api/drops/{id}` | detail dropu |
| POST | `/api/drops` | vytvoření dropu 🔑 |
| PUT | `/api/drops/{id}` | úprava dropu 🔑 |
| DELETE | `/api/drops/{id}` | smazání dropu 🔑 |
| GET | `/api/drops/{dropId}/reservations` | rezervace dropu |
| POST | `/api/drops/{dropId}/reservations` | vytvoření rezervace |

**ReservationsController**

| Metoda | URL | Operace |
|---|---|---|
| GET | `/api/reservations/{id}` | detail rezervace |
| PUT | `/api/reservations/{id}` | úprava rezervace |
| DELETE | `/api/reservations/{id}` | zrušení rezervace |

🔑 = vyžaduje hlavičku `X-Api-Key: sneaker-admin`

### 2. Stavové kódy

Úspěšné odpovědi:

| Situace | Kód |
|---|---|
| úspěšné čtení nebo úprava | 200 |
| úspěšné vytvoření | 201 |
| úspěšné smazání | 204 |

Chybové odpovědi:

| Situace | Kód |
|---|---|
| neplatný vstup (porušené omezení údajů, datum startu v minulosti) | 400 |
| drop ještě nezačal | 400 |
| drop je vyprodaný | 400 |
| snížení počtu kusů pod počet rezervací | 400 |
| mazání dropu, který má rezervace | 400 |
| chybějící nebo špatný API klíč | 401 |
| drop nebo rezervace neexistuje | 404 |
| e-mail už má na tento drop rezervaci | 409 |

Kód 409 použijte jen tehdy, když by vznikl **duplicitní záznam**. Ve všech ostatních případech porušeného pravidla použijte 400.

### 3. Struktura projektu

```
Controllers/   DropsController, ReservationsController
Services/      DropService
Repositories/  DropRepository, ReservationRepository
Models/
  Entities/    Drop, Reservation
  Dtos/        DropRequest, DropResponse, ReservationRequest, ReservationResponse
```

Oba controllery volají jen `DropService`. Služba volá repozitáře a hlídá pravidla. Repozitáře jen ukládají a hledají data.

### 4. Dodatečné informace

**Validace vstupů:** omezení údajů řešte validačními atributy na Request DTO. Díky `[ApiController]` vrátí ASP.NET Core při jejich porušení 400 automaticky.

**Jednoduché datové typy:** `[Required]` na `int`, `decimal` nebo `DateTime` nic nehlídá, protože chybějící hodnota se doplní výchozí hodnotou, například 0. Použijte nullable typy (`int?`, `decimal?`, `DateTime?`).

**Data v repozitáři:** repozitáře registrujte jako **Singleton**. Za běhu aplikace tak existuje jen jedna instance repozitáře a list s daty se mezi požadavky neztratí. List proto nemusí být `static`.
