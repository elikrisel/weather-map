using weather_map.Models;

namespace weather_map.Helpers;

public class Helper
{
    public static string PrintXNumberOfLines(int numberOfLines) => new('=',numberOfLines);
    
    public static List<Statistics> GetStatistics(List<WeatherDataProperties> data, string location) =>
        data.Where(w => w.Location == location).GroupBy(x => x.DateAndTime.Date)
            .Select(g => new Statistics
            {
                Date = g.Key,
                Location = location,
                AverageTemperature = g.Average(w => w.Temperature),
                AverageHumidity = g.Average(w => w.Humidity),
                AverageMold = g.Average(x => x.MoldRisk)
                    
            }).ToList();

    public static void PressToContinue()
    {
        Console.WriteLine("Tryck på valfri tangent för att fortsätta... ");
        Console.ReadKey();
    }
    
}