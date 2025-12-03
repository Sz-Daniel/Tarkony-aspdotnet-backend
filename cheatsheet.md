# ASP.NET Core

### Ctrl+Shift+V

## Http Statuscode

| Kód     | Név                   | ASP.NET Core példa (`TypedResults`)  | Tipikus használat                                 |
| ------- | --------------------- | ------------------------------------ | ------------------------------------------------- |
| **200** | OK                    | `TypedResults.Ok(data)`              | Sikeres lekérés, adat visszaadása                 |
| **201** | Created               | `TypedResults.Created(uri, data)`    | Új erőforrás létrehozva (pl. `POST /todos`)       |
| **202** | Accepted              | `TypedResults.Accepted()`            | Kérés elfogadva, de még feldolgozás alatt         |
| **204** | No Content            | `TypedResults.NoContent()`           | Sikeres, de nincs visszaadott adat (pl. `DELETE`) |
| **400** | Bad Request           | `TypedResults.BadRequest(error)`     | Hibás kliens kérés (pl. rossz JSON)               |
| **401** | Unauthorized          | `TypedResults.Unauthorized()`        | Hitelesítés szükséges                             |
| **403** | Forbidden             | `TypedResults.Forbid()`              | Jogosultság hiánya                                |
| **404** | Not Found             | `TypedResults.NotFound()`            | Erőforrás nem található                           |
| **409** | Conflict              | `TypedResults.Conflict()`            | Ütközés (pl. duplikált adat)                      |
| **422** | Unprocessable Entity  | `TypedResults.UnprocessableEntity()` | Validációs hiba                                   |
| **500** | Internal Server Error | `TypedResults.Problem("hiba")`       | Szerver oldali hiba                               |
| **503** | Service Unavailable   | `Results.StatusCode(503)`            | Szolgáltatás nem elérhető                         |

# C# különleges típusok vs JS6+ – Cheat Sheet példákkal

| C# típus            | Leírás                                                                               | JS/TS megfelelő                                                                                             | C# példa                                                                                                                                                            |
| ------------------- | ------------------------------------------------------------------------------------ | ----------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **record**          | Adatosztály, értékegyenlőséggel. Referencia típus, de értékek alapján hasonlít.      | Nincs natív. Legközelebb: `class` + `Object.is`. Nem ugyanaz, mint JSON obj, mert típusbiztos és immutable. | `public record Todo(int Id, string Name); var t1 = new Todo(1, "Tanulás"); var t2 = new Todo(1, "Tanulás"); Console.WriteLine(t1 == t2); // True`                   |
| **struct**          | Érték típus, **stacken tárolódik**. Másoláskor az érték másolódik, nem a referencia. | JS-ben nincs ilyen, minden objektum referencia típus.                                                       | `public struct Point { public int X; public int Y; } var p1 = new Point { X = 1, Y = 2 }; var p2 = p1; // érték másolódik p2.X = 99; Console.WriteLine(p1.X); // 1` |
| **enum**            | Felsorolási típus, előre definiált konstansok halmaza.                               | JS-ben nincs natív enum, TS-ben van. JS-ben objektummal szimulálható.                                       | `public enum Status { Pending, Completed, Cancelled } Status s = Status.Completed; Console.WriteLine(s); // "Completed"`                                            |
| **List<T>**         | Generikus lista, dinamikusan bővíthető.                                              | JS `Array`.                                                                                                 | `var numbers = new List<int> { 1, 2, 3 }; numbers.Add(4); Console.WriteLine(numbers.Count); // 4`                                                                   |
| **Dictionary<K,V>** | Kulcs‑érték párok tárolására.                                                        | JS `Map` vagy sima objektum.                                                                                | `var dict = new Dictionary<int, string> { {1, "Tanulás"} }; Console.WriteLine(dict[1]); // "Tanulás"`                                                               |
| **HashSet<T>**      | Egyedi elemek halmaza.                                                               | JS `Set`.                                                                                                   | `var set = new HashSet<string> { "A", "B" }; set.Add("A"); // nem duplikál Console.WriteLine(set.Count); // 2`                                                      |
| **Queue<T>**        | FIFO adatszerkezet.                                                                  | JS-ben nincs natív, Array.shift szimulálja.                                                                 | `var q = new Queue<int>(); q.Enqueue(1); q.Enqueue(2); Console.WriteLine(q.Dequeue()); // 1`                                                                        |
| **Stack<T>**        | LIFO adatszerkezet.                                                                  | JS-ben nincs natív, Array.push/pop szimulálja.                                                              | `var s = new Stack<int>(); s.Push(10); s.Push(20); Console.WriteLine(s.Pop()); // 20`                                                                               |
| **string**          | Referencia típus, immutable.                                                         | JS `string`.                                                                                                | `string name = "Hello"; Console.WriteLine(name.ToUpper()); // "HELLO"`                                                                                              |
| **DateTime**        | Pontos idő/dátum kezelés.                                                            | JS `Date`.                                                                                                  | `DateTime now = DateTime.Now; Console.WriteLine(now.ToString("yyyy-MM-dd"));`                                                                                       |
| **decimal**         | Nagy pontosságú szám pénzügyi számításokra.                                          | JS-ben nincs natív, `BigInt` vagy lib.                                                                      | `decimal price = 19.99m; Console.WriteLine(price * 2); // 39.98`                                                                                                    |
| **Guid**            | Globálisan egyedi azonosító.                                                         | JS-ben nincs natív, libekkel generálható.                                                                   | `Guid id = Guid.NewGuid(); Console.WriteLine(id);`                                                                                                                  |

# LINQ vs ES6+ Cheat Sheet

| C# metódus                                  | JS/ES6+ megfelelő              | C# példa                                         | Leírás                                                                 |
| ------------------------------------------- | ------------------------------ | ------------------------------------------------ | ---------------------------------------------------------------------- |
| [**Where**](#where)                         | `array.filter(...)`            | `var evens = list.Where(x => x % 2 == 0);`       | Szűrés, csak a feltételnek megfelelő elemeket adja vissza.             |
| [**Select**](#select)                       | `array.map(...)`               | `var doubled = list.Select(x => x * 2);`         | Leképezés, minden elemet átalakít.                                     |
| [**First**](#first)                         | `array.find(...)`              | `var first = list.First(x => x > 10);`           | Az első elemet adja vissza, ha nincs → kivétel.                        |
| [**FirstOrDefault**](#firstordefault)       | `array.find(...)`              | `var first = list.FirstOrDefault(x => x > 10);`  | Az első elemet adja vissza, ha nincs → default érték.                  |
| [**Single**](#single)                       | nincs direkt megfelelő         | `var only = list.Single(x => x == 42);`          | Pontosan egy elemet ad vissza, ha nincs vagy több van → kivétel.       |
| [**SingleOrDefault**](#singleordefault)     | nincs direkt megfelelő         | `var only = list.SingleOrDefault(x => x == 42);` | Pontosan egy elemet ad vissza, ha nincs → default, ha több van → hiba. |
| [**Any**](#any)                             | `array.some(...)`              | `bool hasBig = list.Any(x => x > 100);`          | Igaz/hamis, van-e legalább egy elem, ami megfelel a feltételnek.       |
| [**All**](#all)                             | `array.every(...)`             | `bool allPositive = list.All(x => x > 0);`       | Igaz/hamis, minden elem megfelel-e a feltételnek.                      |
| [**OrderBy**](#orderby)                     | `array.sort(...)`              | `var ordered = list.OrderBy(x => x);`            | Rendezés növekvő sorrendben.                                           |
| [**OrderByDescending**](#orderbydescending) | `array.sort(...).reverse()`    | `var ordered = list.OrderByDescending(x => x);`  | Rendezés csökkenő sorrendben.                                          |
| [**Count**](#count)                         | `array.length`                 | `int count = list.Count();`                      | Elemszám visszaadása.                                                  |
| [**Sum**](#sum)                             | `array.reduce(...)`            | `int sum = list.Sum();`                          | Összegzés.                                                             |
| [**Average**](#average)                     | `array.reduce(...) / length`   | `double avg = list.Average();`                   | Átlag számítása.                                                       |
| [**Max / Min**](#max--min)                  | `Math.max(...), Math.min(...)` | `int max = list.Max(); int min = list.Min();`    | Maximum / minimum érték.                                               |

# C# vs JS/ES6+ – Párhuzamok Cheat Sheet

| Terület              | JS/ES6+ megfelelő                       | Leírás                                                                     | C# példa                                                                                                                      |
| -------------------- | --------------------------------------- | -------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------- |
| **Aszinkronitás**    | `async/await`, `Promise`, `Promise.all` | Aszinkron műveletek kezelése. C#-ban `Task` és `ValueTask` is van.         | `async Task<int> GetData() { return await service.Fetch(); }`                                                                 |
| **Hibakezelés**      | `try/catch/finally`, `throw Error`      | Szintaxis hasonló, C#-ban gazdag exception hierarchia.                     | `try { DoWork(); } catch(Exception ex) { Console.WriteLine(ex.Message); }`                                                    |
| **Null kezelés**     | `null`, `undefined`, `?.`, `??`         | C#-ban `Nullable<T>`, `?.` (null conditional), `??` (coalescing).          | `string? name = null; Console.WriteLine(name?.ToUpper() ?? "N/A");`                                                           |
| **Függvények**       | `function`, arrow function `x => x*2`   | C#-ban `delegate`, `Func<T>`, `Action<T>`, lambda.                         | `Func<int,int> square = x => x * x; Console.WriteLine(square(5));`                                                            |
| **OOP típusok**      | `class`, `prototype`, TS `interface`    | C#-ban `class`, `interface`, `abstract class`, `record`, `struct`.         | `public interface IAnimal { void Speak(); } public class Dog : IAnimal { public void Speak() => Console.WriteLine("Woof"); }` |
| **JSON kezelés**     | `JSON.stringify`, `JSON.parse`          | JS natív JSON, C#-ban `System.Text.Json` serializer.                       | `var obj = new { Id = 1, Name = "Test" }; string json = JsonSerializer.Serialize(obj);`                                       |
| **Gyűjtemények**     | `Array`, `Map`, `Set`                   | C#-ban `List<T>`, `Dictionary<K,V>`, `HashSet<T>`, `Queue<T>`, `Stack<T>`. | `var dict = new Dictionary<int,string>{{1,"Tanulás"}}; Console.WriteLine(dict[1]);`                                           |
| **Attribútumok**     | TS Decorators `@Component`              | Metaadat osztályokhoz/metódusokhoz. JS-ben csak TS-ben van.                | `[HttpGet] public IActionResult GetAll() => Ok();`                                                                            |
| **Generikusok**      | TS `Array<T>`, `Map<K,V>`               | C#-ban erős generikus támogatás, JS-ben csak TS-ben.                       | `List<string> names = new List<string>(); names.Add("Dani");`                                                                 |
| **Modulok/Névterek** | `import`, `export`                      | C#-ban `namespace`, `using`. JS-ben modulrendszer.                         | `using System; namespace Demo { class Program { static void Main() => Console.WriteLine("Hi"); } }`                           |

# ASP.NET Core – Gyakori Results kombinációk

| Kombináció                             | Példa kód                                                                              | Tipikus használat                                           |
| -------------------------------------- | -------------------------------------------------------------------------------------- | ----------------------------------------------------------- |
| `Results<Ok<T>, NotFound>`             | `app.MapGet("/todos/{id}", Results<Ok<Todo>, NotFound> (int id) => ...);`              | Lekérés: ha van adat → 200 OK, ha nincs → 404               |
| `Results<Created<T>, BadRequest>`      | `app.MapPost("/todos", Results<Created<Todo>, BadRequest> (Todo todo) => ...);`        | Új erőforrás létrehozása: siker → 201, hiba → 400           |
| `Results<NoContent, NotFound>`         | `app.MapDelete("/todos/{id}", Results<NoContent, NotFound> (int id) => ...);`          | Törlés: siker → 204, ha nincs → 404                         |
| `Results<Ok<T>, Conflict>`             | `app.MapPost("/register", Results<Ok<User>, Conflict> (User user) => ...);`            | Regisztráció: siker → 200, ütközés (pl. duplikátum) → 409   |
| `Results<Accepted, Problem>`           | `app.MapPost("/jobs", Results<Accepted, Problem> (Job job) => ...);`                   | Aszinkron feldolgozás: elfogadva → 202, hiba → 500          |
| `Results<Ok<T>, UnprocessableEntity>`  | `app.MapPost("/validate", Results<Ok<Todo>, UnprocessableEntity> (Todo todo) => ...);` | Validáció: siker → 200, hiba → 422                          |
| `Results<Ok<T>, Unauthorized, Forbid>` | `app.MapGet("/secure", Results<Ok<Data>, Unauthorized, Forbid> () => ...);`            | Auth: siker → 200, nincs token → 401, nincs jog → 403       |
| `Results<Ok<T>, Problem>`              | `app.MapGet("/data", Results<Ok<Data>, Problem> () => ...);`                           | Lekérés: siker → 200, szerverhiba → 500                     |
| `Results<StatusCodeHttpResult>`        | `app.MapGet("/health", Results<StatusCodeHttpResult> () => Results.StatusCode(503));`  | Egyedi státuszkód visszaadása (pl. 503 Service Unavailable) |
