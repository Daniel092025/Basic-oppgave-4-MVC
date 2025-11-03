using System.Globalization;

namespace Basic_oppgave_4_MVC;

public class MovieData
{
    public int Rank;
    public string? Title;
    public int Year;
    public double Rating;
    public string Duration;
    public string Genres;

    public MovieData(string csvString)
    {
        var rawData = csvString.Split(',');
        _ = int.TryParse(rawData[0], out Rank);
        Title = rawData[1];
        _ = int.TryParse(rawData[2], out Year);
        _ = double.TryParse(rawData[3], NumberStyles.Any, CultureInfo.InvariantCulture, out Rating);
        Duration = rawData[4]; /* Vet ikke hvorfor, men denne er en string? Inneholder jo tall også. Oppdaget dette tilfeldigvis*/
        Genres = rawData[6];

    }
}