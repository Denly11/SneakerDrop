# Tvorba vlastního API

Vytvořte v ASPAPI pro správu uživatelů a jejich produktů. API musí splňovat specifikaci níže; jeho správnost si ověříte aplikací **User API Test Tool** (viz [Testování](#testování)).

Projekt založte ve Visual Studiu šablonou **ASP.NET Core Web API** se zaškrtnutou volbou *Use controllers*.

## HTTP metody a stavové kódy

Dobré API je předvídatelné: podle metody je jasné, co požadavek udělá, a podle stavového kódu, jak dopadl. Klient tak nemusí číst text odpovědi, aby poznal úspěch nebo druh chyby.

### Metody

| Metoda | Účel | Kód při úspěchu | Tělo odpovědi |
|---|---|---|---|
| `GET` | přečte data, nic nemění | `200 OK` | požadovaná data |
| `POST` | vytvoří nový záznam | `201 Created` | vytvořený záznam včetně přiděleného `id` |
| `PUT` | nahradí celý existující záznam, proto se posílají všechny vlastnosti | `200 OK` | upravený záznam |
| `DELETE` | smaže záznam | `204 No Content` | žádné (proto 204, ne 200) |

`GET`, `PUT` a `DELETE` jsou **idempotentní**: když stejný požadavek pošlete dvakrát, data na serveru dopadnou stejně, jako by přišel jen jednou. `POST` idempotentní není, protože každé volání vytvoří nový záznam.

### Stavové kódy

| Kód | Kdy ho vrátit | V controlleru |
|---|---|---|
| `200 OK` | úspěšné čtení nebo úprava | `return data;` nebo `Ok(data)` |
| `201 Created` | úspěšně vytvořený záznam | `CreatedAtAction(...)` nebo `Created(adresa, data)` |
| `204 No Content` | úspěch, ale není co vrátit (mazání) | `NoContent()` |
| `400 Bad Request` | požadavek je sám o sobě špatně: neplatná data, nevalidní JSON, text místo čísla v parametru | `BadRequest(...)`, při validaci automaticky díky `[ApiController]` |
| `401 Unauthorized` | klient se neprokázal: chybí přihlašovací údaj, nebo je neplatný | `Unauthorized(...)` |
| `403 Forbidden` | víme, kdo je klient, ale na akci nemá právo | `StatusCode(403, ...)` |
| `404 Not Found` | záznam, se kterým má požadavek pracovat, neexistuje | `NotFound(...)` |
| `409 Conflict` | požadavek je v pořádku, ale koliduje s uloženými daty (např. obsazený e-mail) | `Conflict(...)` |
| `500 Internal Server Error` | chyba na straně serveru; běžně vzniká sama při neošetřené výjimce | `StatusCode(500, ...)` |

Kódy, které se často pletou:

- **400 × 404 × 409:** `400` znamená, že požadavek je špatně bez ohledu na uložená data. `404` znamená, že požadavek je v pořádku, ale cílový záznam neexistuje. `409` znamená, že požadavek je v pořádku a záznam existuje, ale provedení by porušilo pravidlo nad uloženými daty.
- **401 × 403:** `401` znamená „nevím, kdo jsi“, `403` znamená „vím, kdo jsi, ale tohle nesmíš“.
- **`Forbid()` nepoužívejte.** Funguje jen s nastaveným systémem autentizace ASP.NET Core a bez něj vyhodí výjimku. Pro 403 použijte `StatusCode(403, ...)`.

## Obecná pravidla

Platí pro všechny úkoly, pokud není u úkolu uvedeno jinak.

- **Základní adresa** všech endpointů je `/api/users`.
- **Formát dat:** požadavky i odpovědi jsou ve formátu JSON a názvy vlastností píšeme PascalCase (`username`, `userId`, …). To je výchozí chování ASP.NET Core.
- **Ukládání dat:** stačí data držet v paměti (např. `List<T>` nebo `Dictionary<int, T>` ve službě registrované jako singleton). Databáze není potřeba.
- **ID** přiděluje server automaticky: celá čísla od 1, každý nový záznam dostane o 1 vyšší ID. ID se po smazání znovu nepoužívají.
- **Autentizace** se provádí hlavičkou požadavku. Pokud hlavička chybí, vrací se `401 Unauthorized`. Pokud má hlavička špatnou hodnotu, vrací se `401 Unauthorized`, u hlavičky `Admin` ale `403 Forbidden`. Hlavičku načtěte parametrem akce, např. `[FromHeader(Name = "Client-Id")] string? clientId`. Typ musí být `string?`, jinak ASP.NET Core vyhodnotí chybějící hlavičku jako chybu validace a vrátí 400 místo 401.
- **Pořadí kontrol:** validace vstupu (400) → autentizace (401/403) → existence záznamu (404) → konflikt s uloženými daty (409). Validaci provede `[ApiController]` automaticky ještě před zavoláním akce. V akci pak stačí kontrolovat hlavičku, existenci záznamu a konflikt, v tomto pořadí.
- **Chybové odpovědi** (400, 401, 403, 404, 409) mají tělo s popisem chyby, např. `{ "error": "Chybí hlavička ApiKey." }`. U chyb validace stačí výchozí odpověď, kterou vrací atribut `[ApiController]`.

| Hlavička | Správná hodnota | Kde se používá |
|---|---|---|
| `Client-Id` | `Client123` | vytvoření uživatele |
| `ApiKey` | `Secret123` | čtení a úprava uživatele, vytvoření a úprava produktu |
| `Admin` | `admin123` | mazání uživatele a produktu |

## Entity

### User

| Vlastnost | Typ | Pravidla |
|---|---|---|
| `id` | `int` | přiděluje server |
| `username` | `string` | povinné, 3–50 znaků |
| `email` | `string` | povinné, musí obsahovat znak `@`; **jedinečné** – dva uživatelé nesmí mít stejný e-mail (bez ohledu na velikost písmen) |
| `age` | `int` | povinné, celé číslo 1–150 |

```json
{ "id": 1, "username": "jan.novak", "email": "jan@example.com", "age": 25 }
```

### Product

| Vlastnost | Typ | Pravidla |
|---|---|---|
| `id` | `int` | přiděluje server |
| `name` | `string` | povinné, nejvýše 100 znaků |
| `price` | `decimal` | povinné, větší než 0 a nejvýše 1 000 000 |
| `category` | `string` | volitelné, nejvýše 50 znaků |
| `userId` | `int` | ID vlastníka, nastaví server podle URL |

```json
{ "id": 1, "name": "Notebook", "price": 1500.00, "category": "Elektronika", "userId": 1 }
```

Produkt vždy patří jednomu uživateli. Smazáním uživatele se smažou i jeho produkty.

## Validace vstupu

Pravidla z tabulek entit platí pro vytvoření i úpravu. Jejich porušení vrací `400 Bad Request`, kromě obsazeného e-mailu, který vrací `409 Conflict`.

- Většinu pravidel zapíšete validačními atributy z `System.ComponentModel.DataAnnotations` přímo na vlastnosti třídy, která přijímá tělo požadavku, např. `[Required]`, `[StringLength]` nebo `[Range]`. Controller s atributem `[ApiController]` pak data zkontroluje sám a při chybě vrátí 400 s popisem, ještě než se zavolá vaše metoda.
- **Pozor na `string` a `string?`.** V projektu se zapnutými nullable typy je vlastnost typu `string` automaticky povinná. Volitelná vlastnost (`category`) proto musí být `string?`, jinak API odmítne produkt bez kategorie.
- **Pozor na `[Range]` u typu `decimal`.** `[Range(0, 1000000)]` porovnává jako `int` a cenu před porovnáním zaokrouhlí, takže `1000000.01` projde. Použijte variantu s typem: `[Range(typeof(decimal), "0", "1000000")]`.
- Nevalidní JSON nebo text místo čísla (v těle i v parametru URL) vrátí 400 automaticky.
- **Jedinečnost e-mailu atributem nezkontrolujete,** protože závisí na už uložených datech. Ověřte ji v kódu a vraťte `Conflict(...)`. Při úpravě uživatele smí uživatel ponechat svůj vlastní e-mail.

## Přehled endpointů

| Úkol | Metoda | URL | Autentizace |
|---|---|---|---|
| 1 | `POST` | `/api/users/create` | `Client-Id` |
| 2 | `GET` | `/api/users/{id}` | `ApiKey` |
| 3 | `GET` | `/api/users/search` | – |
| 4 | `PUT` | `/api/users/{id}` | `ApiKey` |
| 5 | `DELETE` | `/api/users/{id}` | `Admin` |
| 6 | `POST` | `/api/users/{userId}/products` | `ApiKey` |
| 7 | `GET` | `/api/users/{userId}/products` | – |
| 8 | `PUT` | `/api/users/{userId}/products/{productId}` | `ApiKey` |
| 9 | `DELETE` | `/api/users/{userId}/products/{productId}` | `Admin` |

> **Tip:** Cesty `/api/users/search` a `/api/users/{id}` se překrývají. Omezte parametr na číslo (`{id:int}`), aby se `search` nebral jako ID.

## Úkoly

### Úkol 1 – Vytvoření uživatele

`POST /api/users/create`

- **Autentizace:** hlavička `Client-Id: Client123`
- **Tělo požadavku:**
  ```json
  { "username": "jan.novak", "email": "jan@example.com", "age": 25 }
  ```
- **Odpovědi:**
  - `201 Created` – vytvořený uživatel včetně přiděleného `id`
  - `401 Unauthorized` – hlavička `Client-Id` chybí nebo má jinou hodnotu než `Client123`
  - `400 Bad Request` – data nesplňují pravidla entity User
  - `409 Conflict` – e-mail už používá jiný uživatel

### Úkol 2 – Získání uživatele podle ID

`GET /api/users/{id}`

- **Autentizace:** hlavička `ApiKey: Secret123`
- **Odpovědi:**
  - `200 OK` – data uživatele
  - `401 Unauthorized` – klíč chybí nebo je neplatný
  - `404 Not Found` – uživatel s daným ID neexistuje

### Úkol 3 – Vyhledávání uživatelů

`GET /api/users/search`

- **Autentizace:** žádná
- **Volitelné parametry v URL** (lze kombinovat, např. `/api/users/search?name=jan&min_age=18&limit=5`):

  | Parametr | Typ | Význam |
  |---|---|---|
  | `name` | `string` | uživatelské jméno obsahuje zadaný text (bez ohledu na velikost písmen) |
  | `min_age` | `int` | věk je alespoň zadaná hodnota |
  | `max_age` | `int` | věk je nejvýše zadaná hodnota |
  | `limit` | `int` | vrátí nejvýše tolik uživatelů; musí být větší než 0 |

- **Odpovědi:**
  - `200 OK` – pole nalezených uživatelů (může být prázdné)
  - `400 Bad Request` – neplatný parametr, např. text místo čísla (`min_age=abc`) nebo `limit=0`

### Úkol 4 – Úprava uživatele

`PUT /api/users/{id}`

- **Autentizace:** hlavička `ApiKey: Secret123`
- **Tělo požadavku** (všechny vlastnosti povinné, stejná pravidla jako u vytvoření):
  ```json
  { "username": "jan.novak2", "email": "novy@example.com", "age": 26 }
  ```
- **Odpovědi:**
  - `200 OK` – aktualizovaný uživatel
  - `401 Unauthorized` – klíč chybí nebo je neplatný
  - `400 Bad Request` – neplatná data
  - `404 Not Found` – uživatel neexistuje
  - `409 Conflict` – e-mail používá jiný uživatel (ponechat si vlastní e-mail je v pořádku)

### Úkol 5 – Smazání uživatele

`DELETE /api/users/{id}`

- **Autentizace:** hlavička `Admin: admin123`
- **Odpovědi:**
  - `204 No Content` – uživatel (i jeho produkty) byl smazán; odpověď nemá tělo
  - `401 Unauthorized` – hlavička `Admin` chybí
  - `403 Forbidden` – hlavička `Admin` má jinou hodnotu než `admin123`
  - `404 Not Found` – uživatel neexistuje

### Úkol 6 – Vytvoření produktu uživatele

`POST /api/users/{userId}/products`

- **Autentizace:** hlavička `ApiKey: Secret123`
- **Tělo požadavku:**
  ```json
  { "name": "Notebook", "price": 1500.00, "category": "Elektronika" }
  ```
- **Odpovědi:**
  - `201 Created` – vytvořený produkt včetně přiděleného `id` a `userId`
  - `401 Unauthorized` – klíč chybí nebo je neplatný
  - `400 Bad Request` – data nesplňují pravidla entity Product
  - `404 Not Found` – uživatel neexistuje

### Úkol 7 – Seznam produktů uživatele

`GET /api/users/{userId}/products`

- **Autentizace:** žádná
- **Volitelné parametry v URL:**

  | Parametr | Typ | Význam |
  |---|---|---|
  | `min_price` | `decimal` | cena je alespoň zadaná hodnota |
  | `max_price` | `decimal` | cena je nejvýše zadaná hodnota |
  | `sort` | `string` | řazení podle ceny: `asc` (vzestupně) nebo `desc` (sestupně) |

- **Odpovědi:**
  - `200 OK` – pole produktů uživatele (může být prázdné)
  - `400 Bad Request` – neplatný parametr, např. `min_price=abc` nebo `sort=xyz`
  - `404 Not Found` – uživatel neexistuje
  - `500 Internal Server Error` – **speciální případ** na procvičení serverové chyby: pro `userId = 999` vždy, i když uživatel s tímto ID existuje. Kontrolu proto proveďte dřív než kontrolu existence uživatele.

### Úkol 8 – Úprava produktu

`PUT /api/users/{userId}/products/{productId}`

- **Autentizace:** hlavička `ApiKey: Secret123`
- **Tělo požadavku** (stejná pravidla jako u vytvoření):
  ```json
  { "name": "Notebook Pro", "price": 1999.99, "category": "Elektronika" }
  ```
- **Odpovědi:**
  - `200 OK` – aktualizovaný produkt
  - `401 Unauthorized` – klíč chybí nebo je neplatný
  - `400 Bad Request` – neplatná data
  - `404 Not Found` – uživatel neexistuje, produkt neexistuje, nebo produkt patří jinému uživateli

### Úkol 9 – Smazání produktu

`DELETE /api/users/{userId}/products/{productId}`

- **Autentizace:** hlavička `Admin: admin123`
- **Odpovědi:**
  - `204 No Content` – produkt byl smazán; odpověď nemá tělo
  - `401 Unauthorized` – hlavička `Admin` chybí
  - `403 Forbidden` – hlavička `Admin` má jinou hodnotu než `admin123`
  - `404 Not Found` – uživatel neexistuje, produkt neexistuje, nebo produkt patří jinému uživateli

## Povolení CORS

Testovací aplikace běží v prohlížeči na jiné adrese než vaše API, a prohlížeč proto její požadavky zablokuje, pokud je API výslovně nepovolí (CORS). Bez toho selžou všechny testy s chybou *Could not reach the API*.

Do `Program.cs` přidejte registraci CORS a middleware `app.UseCors()`. Middleware musí být **před** `app.MapControllers()`:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Povolí požadavky z libovolné adresy, s libovolnými hlavičkami a metodami.
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();

app.Run();
```

> Takto otevřené CORS je v pořádku pro vývoj a tuto úlohu, ale ne pro produkční API, kde se mají povolit jen konkrétní adresy.

## Testování

Aplikace **User API Test Tool** pošle na vaše API sadu požadavků. U každého zkontroluje stavový kód a u úspěšných odpovědí i obsah: zda vrácená data odpovídají odeslaným, zda filtry a řazení vracejí správné záznamy a zda se změny opravdu uložily.

1. Spusťte své API a zjistěte jeho adresu (v `Properties/launchSettings.json`, profil `https`, např. `https://localhost:7056`).
2. Otevřete aplikaci **User API Test Tool**
3. Do pole **API Base URL** zadejte adresu API včetně cesty, např. `https://localhost:7056/api/users`.
4. Klikněte na **Run All Tests**, nebo spusťte jednotlivé úkoly tlačítkem **Run**.

Užitečné vědět:

- Testy si vytvářejí vlastní uživatele a produkty a k tomu používají endpointy z úkolů 1 a 6. Dokud tyto úkoly nefungují, selžou i úkoly, které na nich závisí, s hláškou *Setup failed*.
- Některé testy ověřují výsledek čtením dat: úkoly 4 a 5 přes endpoint z úkolu 2, úkoly 8 a 9 přes endpoint z úkolu 7. Proto je nejlepší dělat úkoly popořadě.
- Každé spuštění testů používá nová jména a e-maily, takže testy lze pouštět opakovaně bez restartu API.
- Testy neexistujících záznamů používají ID `99999`.
- Na každou odpověď se čeká nejvýše 1 sekundu.
- Pokud testy hlásí *Could not reach the API*, zkontrolujte CORS (viz výše) a to, že API běží na zadané adrese.

