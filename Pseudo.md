# Pseudo Pseudo Pseeeeudo

### Henter ut dataen fra CSV i "Data" mappen:

```csharp
public class MovieData
{
    public int Rank;
    public string? Title;
    public int Year;
    public double Rating;
    public string Duration;
    public string Genres;
}
```

#### Constructor og parsing av verdiene

```csharp
public MovieData(string csvString)
    {
        var rawData = csvString.Split(',');
        _ = int.TryParse(rawData[0], out Rank);
        Title = rawData[1];
        _ = int.TryParse(rawData[2], out Year);
        _ = double.TryParse(rawData[3], NumberStyles.Any, CultureInfo.InvariantCulture, out Rating);
        Duration = rawData[4]; /* Vet ikke hvorfor, men denne er en string? Inneholder jo tall også. Oppdaget dette tilfeldigvis. Tenkte jeg måtte formatere om til minutter osv. Men leser bare teksten (og tallet rett ut). Tenkte det går fint, siden jeg søker dette opp eller henter ut data. Annet en bare det som står der.*/
        Genres = rawData[6];

    }
```

## Program

#### Her henter jeg ut selve csv filen, lager meny som bruker kan bruke for å dele opp filen.

```csharp
var filePath = args.Length > 0 ? args[0] : "imdb_top_movies.csv";

    if (!File.Exists(filePath))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Finner ikke filen: {filePath}");
        Console.ResetColor();
        return;
    }

    var csvString = File.ReadAllLines(filePath);
    var movies = csvString.Skip(1).Select(csvString => new MovieData(csvString)).ToList();
```

#### Gir brukere valg, 1-5 igjennom en switch case.

```csharp
switch (valg)
            {
                case "Liste":
                case "1":
                    Console.WriteLine("\n Topp filmer:\n");
                    foreach (var movie in movies.Take(10))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"🔥{movie.Rank}. {movie.Title} ({movie.Year}) - {movie.Rating}⭐   {movie.Duration}");
                        Console.ResetColor();
                    }
                    break;

                case "Full liste":
                case "2":
                    Console.WriteLine("\n Hele top 250:\n");
                    foreach (var movie in movies)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"🔥{movie.Rank}. {movie.Title} ({movie.Year}) ({movie.Genres}) - {movie.Rating}⭐");
                        Console.ResetColor();
                    }
                    break;
            }
```

#### En av switch casene har en .Where og .Select i seg. Er en søke funksjon som leter etter ordet som blir skrevet og velger "select" filmen eller filmer som har søkeordet i seg.

```csharp
case "søk":
                case "4":
                    Console.WriteLine("Skriv inn et søkeord");
                    string søkeord = Console.ReadLine() ?? "".Trim().ToLower();

                    var treff = movies
                        .Where(m => m.Title != null && m.Title.ToLower().Contains(søkeord))
                        .Select(m => $"🔥{m.Rank}. {m.Title} {m.Year} - {m.Rating} ⭐");

                    if (treff.Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nFant {treff.Count()} filmer som matcher '{søkeord}': \n");
                        foreach (var film in treff.Take(10))
                        Console.WriteLine(film);
                        Console.ResetColor();
                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Ingen filmer matchet '{søkeord}'");
                        Console.ResetColor();
                    }
                    break;

```

#### Flyttet menu valget til egen modell // MenuService

```csharp
public string VisMenu()
{
   {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n Vennligst velg ett alternativ");
            Console.WriteLine("1. Liste - Top 10");
            Console.WriteLine("2. Full liste - Hele listen på top 250");
            Console.WriteLine("3. Sjangere - Mest populære sjangere");
            Console.WriteLine("4. Søk");
            Console.WriteLine("5. Exit - Avslutt programmet");
            Console.Write("\nDitt valg: ");
            Console.ResetColor();
            return Console.ReadLine() ?? "".Trim();
    }
}

```



#### Med masse farger og en ACII (ofcourse, blitt standard nå)
    - Men nå markert med "expression-based string" aka """ og ikke @.
