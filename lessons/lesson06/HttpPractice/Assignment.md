# HTTP Playground – Zadání

Cílem je naučit se číst a posílat HTTP požadavky a rozumět odpovědím serveru.

## Jak odpovídat

- Odpovědi vpisujte přímo do tohoto dokumentu: za dvojtečku, do prázdných buněk tabulek, za *Odpověď:* a do prázdných bloků kódu (sem vložte celé tělo odpovědi nebo výpis hlaviček).
- Ke každému úkolu zapište **skutečně zjištěné hodnoty**: status kód (číslo i název, např. *404 Not Found*), požadované hlavičky a tělo odpovědi.
- Pokud je tělo odpovědi prázdné, napište „prázdné“.
- V příkladech nahraďte `<server>` adresou serveru.

## Nástroje

- **Hoppscotch:** Online nástroj pro zasílání HTTP požadavků - https://hoppscotch.io/.
  - Metodu a URL zadáte nahoře, hlavičky v záložce *Headers*, tělo v záložce *Body* (typ obsahu vyberete v rozbalovacím seznamu *Content Type*).
  - Hlavičky odpovědi najdete pod odpovědí v záložce *Headers*.
- **Prohlížeč:** F12 → záložka *Network* → klikněte na požadavek a podívejte se na *Headers* a *Response*.
  - Zaškrtněte *Preserve log*, aby se seznam požadavků nemazal při přesměrování.

---

## 1. HTTP komunikace

**a)** Otevřete v prohlížeči nástroje pro vývojáře (F12) na záložce *Network* a pak načtěte `<server>/hello`. U požadavku zapište:

- Metoda: **GET**
- Celá URL: **https://http-api-646982638495.europe-west1.run.app/hello**
- Status kód: **200**
- Verze protokolu (sloupec *Protocol* zapnete pravým kliknutím na záhlaví tabulky): h3
- Alespoň 3 hlavičky **požadavku**, které prohlížeč poslal sám od sebe, aniž jste je zadali:

```text
Authority; Method; Path
```

**b)** Takto vypadá celá komunikace s endpointem `POST /echo` v textové podobě. Zachytil ji nástroj cURL příkazem `curl -v <server>/echo -H "Content-Type: application/json" -d '{"name": "Test", "value": 123}'`.

Požadavek:

```http
POST /echo HTTP/1.1
Host: localhost:5242
User-Agent: curl/8.11.0
Accept: */*
Content-Type: application/json
Content-Length: 30

{"name": "Test", "value": 123}
```

Odpověď:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
Date: Sat, 03 Oct 2026 18:53:16 GMT
Server: Kestrel
Transfer-Encoding: chunked

{"name":"Test","value":123}
```

Z jakých tří částí se skládá první řádek požadavku a z jakých tří částí první řádek odpovědi?

- První řádek požadavku: **http metoda, cesta, verze http**
- První řádek odpovědi: **verze http, stavový kód, popis stavového kódu**

Jak souvisí hodnota `Content-Length: 30` s tělem požadavku?

*Odpověď:* **délka těla**

Co odděluje hlavičky od těla zprávy?

*Odpověď:* **prázdný řádek**

Která hlavička požadavku říká serveru, v jakém formátu je tělo, a která říká, jaký formát odpovědi klient přijme?

- Formát těla: **Content-Type**
- Přijímaný formát odpovědi: **Accept**

## 2. GET /hello

Pošlete požadavek z Hoppscotche. Zapište status kód, hodnotu hlavičky `Content-Type` a celé tělo odpovědi. Pošlete požadavek znovu po několika sekundách: která část odpovědi se změnila?

- Status kód: **200**
- `Content-Type`: **text/plain; charset=utf-8**
- Tělo odpovědi: 

```text 
nemá
```

- Část odpovědi, která se změnila: **čas**

## 3. GET /page

**a)** Pošlete požadavek z Hoppscotche. Zapište hodnotu hlavičky `Content-Type`, přesný text v elementu `<title>` a text nadpisu `<h1>`.

*to je teda ASI teda pro tu adresu ze zadání...*
- `Content-Type`: **application/json || text/html**
- Text v `<title>`: **nic tam není/pokud pro poskytnutou adresu: Welcome Page**
- Text v `<h1>`: **taky není /--||-- Welcome to the page!**

**b)** Otevřete stejnou adresu v prohlížeči. Popište, čím se zobrazení liší od Hoppscotche. Která hlavička prohlížeči říká, že má obsah vykreslit jako HTML stránku?

- Rozdíl v zobrazení: **grafická interpretace kódu co tam byl vypsaný**
- Hlavička: **Content-Type**

## 4. GET /butterfly

Zapište status kód, hodnotu `Content-Type` a celé tělo odpovědi. (Výsledek budete potřebovat v úkolu 17.)

- Status kód: **404**
- `Content-Type`: **application/json; charset=utf-8**
- Tělo odpovědi:

```text
{
  "error": "Butterfly not found",
  "code": 404
}
```

## 5. GET /data

**a)** V prohlížeči otevřete DevTools na záložce *Network* (se zaškrtnutým *Preserve log*) a načtěte `<server>/data`. Kolik požadavků se v seznamu objevilo? U každého zapište URL a status kód. U prvního zapište i hodnotu hlavičky odpovědi `Location`.

- Počet požadavků:

| # | URL | Status kód |
| --- | --- | --- |
| 1 | https://http-api-646982638495.europe-west1.run.app/data | 302 | 
| 2 | https://http-api-646982638495.europe-west1.run.app/data-temp | 200 |
| 3 | | |

Nepotřebné řádky nechte prázdné.

- `Location` u prvního požadavku: **/data-temp**

**b)** Jaká URL je po načtení v adresním řádku?

*Odpověď:* **https://http-api-646982638495.europe-west1.run.app/data-temp**

___
**!** Já si myslím že je něco špatně, nejdřív mi to načítalo /data a vracelo data a pakto switchnulo na /data-temp a vracelo data-temp ale hádám že bych měl vidět obě ale ted už se mi ty data prsotě nezobrazují
Ale ted jsem to otevřel v anonymním okně a jde to...

**c)** Pošlete `GET /data` z Hoppscotche. Jaký status kód a jaký obsah zobrazí? Porovnejte je s prvním požadavkem z a) a vysvětlete rozdíl.

- Status kód: **200**
- Obsah: **"message": "Temporary response",
  "id": "54f2c220-7a07-436f-abe3-bcc1239d02c1"**
- Vysvětlení rozdílu:**už se to načítá rovnou to /data-temp a ne /data**

**d)** Ze kterého endpointu obsah ve skutečnosti pochází?

*Odpověď:* **z data**

## 6. GET /permanent

**a)** Postupujte stejně jako v úkolu 5a a zapište URL, status kódy a hodnotu `Location`.

| # | URL | Status kód |
| --- | --- | --- |
| 1 | https://http-api-646982638495.europe-west1.run.app/permanent | 301 |
| 2 | https://http-api-646982638495.europe-west1.run.app/permanent-storage | 200 |
| 3 | | |

Nepotřebné řádky nechte prázdné.

- `Location` u prvního požadavku: **/permanent-storage**

**b)** Načtěte `<server>/permanent` v prohlížeči ještě jednou (zadejte adresu znovu do adresního řádku). Co se teď u požadavku `/permanent` zobrazí ve sloupcích *Status* a *Size*? Odkud prohlížeč odpověď vzal?

- *Status*: **200**
- *Size*: **0.1kB**
- Odkud prohlížeč odpověď vzal: **vzal přesměrování z cache a rovnou načetl /permanent-storage**

**c)** Porovnejte status kódy prvních požadavků z úkolů 5a a 6a. Jaký je mezi nimi rozdíl z pohledu prohlížeče?

*Odpověď:* **302 je dočasné přesměrování a 301 trvalé přesměrování**

## 7. GET /file

Otevřete `<server>/file` v prohlížeči s otevřenými DevTools. Zapište hodnoty hlaviček odpovědi `Content-Type` a `Content-Disposition`, název staženého souboru a jeho celý obsah. Která hlavička způsobí, že prohlížeč soubor stáhne místo toho, aby ho zobrazil?

- `Content-Type`: **text/plain**
- `Content-Disposition`: attachment; filename=sample.txt; filename*=UTF-8''sample.txt
- Název staženého souboru: **sample.txt**
- Obsah souboru:

```text
This is a sample file content.
Downloaded at: 10/13/2026 15:02:27
```

- Hlavička, která způsobí stažení: **content-disposition**

## 8. POST /echo

Posílejte z Hoppscotche. U každé varianty zapište status kód (číslo i název) a tělo odpovědi:

- a) Tělo `{"name": "Test", "value": 123}` s typem obsahu `application/json`.
- b) Stejně jako a), ale do JSONu přidejte vlastní pole.
- c) Stejné tělo jako a), ale s typem obsahu `text/plain`.
- d) Tělo `{"name": Test}` s typem obsahu `application/json`.

Tělo, které jste poslali ve variantě b):

```json
{"name": "Test", "value": 123, "bonita": "výborná"}
```

| Varianta | Status kód | Tělo odpovědi |
| --- | --- | --- |
| a) | **200**| **{ "name": "Test", "value": 123 }** |
| b) | **200** | **{ "name": "Test", "value": 123, "bonita": "výborná" }**|
| c) | **415** | **nic** |
| d) | **200**| **"{\"name\": Test}"** |

Porovnejte odpovědi variant c) a d) s variantou a). Na čí straně je podle status kódu příčina: u klienta, nebo u serveru?

*Odpověď:* **u c to je nepodporovaný typ obsahu a u d je to špatné tělo**

## 9. POST /status/{kód}

Z Hoppscotche pošlete na každou adresu prázdné tělo `{}` s typem obsahu `application/json`. U každého volání zapište status kód (číslo i název) a tělo odpovědi.

| Adresa | Status kód | Tělo odpovědi |
| --- | --- | --- |
| `/status/200` | **200 OK** | **{"message": "Success","body": null}** |
| `/status/201` | **201 Created** | {"message": "Created", "body": null}|
| `/status/400` | **400 Bad Request**| **{  "error": "Bad Request",  "body": null}** |
| `/status/401` | **401 Unauthorized**| **nic** |
| `/status/403` | **403 Forbiden**| **nic** |
| `/status/404` | **404 Not Found** | **{ "error": "Not Found"}** |
| `/status/500` | **500 Internal Server Error**| **{"type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",  "title": "An error occurred while processing your request.""status":500, "detail": "Internal Server Error"}** |
| `/status/XXX` (XXX zvolte z rozsahu 600–999) | **404 Not Found** | **nic** |

Dále zapište:

- Hodnota hlavičky `Location` u `/status/201`:
- K čemu klientovi hlavička `Location` slouží:
- Hodnota hlavičky `WWW-Authenticate` u `/status/401`:
- K čemu klientovi hlavička `WWW-Authenticate` slouží:
- Zvolený kód XXX:
- Kódy, které server podle odpovědi podporuje:

Vysvětlete vlastními slovy rozdíl mezi 401 a 403. Uveďte příklad situace, kdy by server měl vrátit 401, a příklad, kdy 403.

- Rozdíl:
- Příklad pro 401:
- Příklad pro 403:

## 10. GET /users

U každé varianty zapište status kód a tělo odpovědi:

| Varianta | Adresa | Status kód | Tělo odpovědi |
| --- | --- | --- | --- |
| a) | `/users?name=Jan` | | |
| b) | `/users?name=Jana&age=25` | | |
| c) | `/users?name=Jana&age=abc` | | |
| d) | `/users` (bez parametrů) | | |

Který parametr je povinný, který volitelný a podle čeho jste to poznali? Jakou hodnotu dostane `age`, když ho nezadáte? Proč server ve variantě c) vrátil právě tento status kód?

- Povinný parametr:
- Volitelný parametr:
- Podle čeho jste to poznali:
- Hodnota `age`, když ho nezadáte:
- Proč server ve variantě c) vrátil tento status kód:

## 11. GET /users se zvláštními znaky

Zadávejte adresy do adresního řádku prohlížeče s otevřenými DevTools. U každé varianty zapište hodnotu `name` z odpovědi:

| Varianta | Adresa | `name` v odpovědi |
| --- | --- | --- |
| a) | `<server>/users?name=Tom&Jerry` | |
| b) | `<server>/users?name=Tom%26Jerry` | |
| c) | `<server>/users?name=Jan Novák` (s mezerou a diakritikou) | |

U varianty c) zapište hodnotu *Request URL* z DevTools: jak prohlížeč adresu před odesláním upravil?

- *Request URL*:

Porovnejte jméno, které jste chtěli poslat, se jménem v odpovědi. Vysvětlete výsledek varianty a). Co znamenají zápisy `%26`, `%20` a `%C3%A1`? Jak byste do URL zapsali jméno `Anna Černá`?

- Vysvětlení varianty a):
- `%26`:
- `%20`:
- `%C3%A1`:
- `Anna Černá` v URL:

## 12. GET /secure-data

Posílejte z Hoppscotche, hlavičky zadávejte v záložce *Headers*. U každé varianty zapište status kód a tělo odpovědi. U všech variant, jejichž odpověď obsahuje hlavičku `WWW-Authenticate`, zapište i její hodnotu.

| Varianta | Hlavička požadavku | Status kód | Tělo odpovědi | `WWW-Authenticate` |
| --- | --- | --- | --- | --- |
| a) | bez hlavičky `Api-Key` | | | |
| b) | `Api-Key: secret123` | | | |
| c) | `Api-Key: wrong` | | | |
| d) | `api-key: secret123` (název hlavičky malými písmeny) | | | |
| e) | `Api-Key: SECRET123` (hodnota velkými písmeny) | | | |

Z výsledků d) a e) odvoďte, kde záleží na velikosti písmen: v názvu hlavičky, nebo v její hodnotě?

*Odpověď:*

## 13. POST /special-echo

Posílejte z Hoppscotche tělo `{"message": "test"}` s typem obsahu `application/json`. U každé varianty zapište status kód a přesný text odpovědi:

| Varianta | Hlavičky | Status kód | Text odpovědi |
| --- | --- | --- | --- |
| a) | `Special-Request: true` a `Custom-Username: username` | | |
| b) | bez hlavičky `Special-Request` | | |
| c) | bez hlavičky `Custom-Username` | | |
| d) | `Special-Request: false` | | |

Porovnejte odpovědi variant b)–d). Co se z nich klient dozví o tom, co má v požadavku opravit? Navrhněte text odpovědi, který by mu pomohl víc.

- Co se klient dozví:
- Návrh lepší odpovědi:

## 14. PUT /users/{id}

**a)** Pošlete na `/users/42` tělo `{"name": "Nové jméno"}`. Zapište status kód a tělo odpovědi.

- Status kód:
- Tělo odpovědi:

```text

```

**b)** Pošlete stejný požadavek na `/users/abc`. Zapište status kód a tělo odpovědi, porovnejte je s a) a výsledek vysvětlete.

- Status kód:
- Tělo odpovědi:
- Vysvětlení:

## 15. DELETE /users/99

Zapište status kód a tělo odpovědi. Pak stejný požadavek pošlete podruhé a zapište odpověď. Co z porovnání obou odpovědí plyne o tom, jestli server uživatele skutečně ukládá a maže?

| Volání | Status kód | Tělo odpovědi |
| --- | --- | --- |
| 1. | | |
| 2. | | |

*Odpověď:*

## 16. GET /data-temp

Pošlete `/data-temp` třikrát za sebou a zapište všechny tři hodnoty `id`. Porovnejte je. Co z toho plyne o tom, jestli si server mezi požadavky něco pamatuje?

| Volání | `id` |
| --- | --- |
| 1. | |
| 2. | |
| 3. | |

*Odpověď:*

## 17. GET na další adresy

**a)** Z Hoppscotche pošlete metodou GET požadavek na `/echo`, `/special-echo` a `/status/200`. U každého zapište status kód (číslo i název) a hodnotu hlavičky `Allow`, pokud ji odpověď obsahuje.

| Adresa | Status kód | `Allow` |
| --- | --- | --- |
| `/echo` | | |
| `/special-echo` | | |
| `/status/200` | | |

**b)** Pošlete GET na `/neexistuje` a zapište status kód a tělo odpovědi.

- Status kód:
- Tělo odpovědi:

**c)** Vysvětlete rozdíl mezi odpověďmi z a) a b).

*Odpověď:*

**d)** Porovnejte odpověď z b) s odpovědí z úkolu 4 (`/butterfly`). V čem se shodují, v čem se liší a co z toho vyčtete o tom, jestli adresa na serveru existuje?

- Shoda:
- Rozdíl:
- Co z toho vyčtete:
