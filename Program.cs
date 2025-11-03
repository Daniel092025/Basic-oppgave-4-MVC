namespace Basic_oppgave_4_MVC;

using System.Collections.Immutable;
using System.IO.Compression;

class Program
{
    static void Main(string[] args)
    {
        
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

    Console.WriteLine($"✅ Lest inn {movies.Count} filmer fra {filePath}");

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\n IMDB top movies");
        Console.ResetColor();

        bool fortsett = true;
        while (fortsett)
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

            string valg = (Console.ReadLine() ?? "").Trim().ToLower();

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

                case "Sjangere":
                case "3":
                    Console.WriteLine("\n Mest populære sjangere:\n");

                    var mestPopulæreSjanger = movies
                                .GroupBy(m => m.Genres)
                                .OrderByDescending(g => g.Count())
                                .Take(4);
                    foreach(var gruppe in mestPopulæreSjanger)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{gruppe.Key} - {gruppe.Count()} filmer");
                        Console.ResetColor();
                    }
                    break;

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


                case "exit":
                case "5":
                    fortsett = false;
                    Console.WriteLine("Avslutter...");
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ugyldig valg, prøv igjen!");
                    Console.ResetColor();
                    break;

            }

    
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n Thank you for choosing and trusting\n");
        Console.WriteLine();
        Console.WriteLine(""" 
 ██████╗██╗   ██╗██████╗ ███████╗██████╗ ███╗   ██╗███████╗████████╗
██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗████╗  ██║██╔════╝╚══██╔══╝
██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██╔██╗ ██║█████╗     ██║   
██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██║╚██╗██║██╔══╝     ██║   
╚██████╗   ██║   ██████╔╝███████╗██║  ██║██║ ╚████║███████╗   ██║   
 ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝   ╚═╝  
""");
        Console.ResetColor();
    }
}
