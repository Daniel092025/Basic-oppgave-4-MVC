namespace Basic_oppgave_4_MVC.Service;

public class MenuService
{
    public string VisMenu()
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