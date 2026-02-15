using weather_map.Helpers;

namespace weather_map;

public class SubMenu
{
    public static void GetAverageTempForSelectedDate(List<WeatherDataProperties> data,string location)
    {
        // Välj Datum mellan 2016-05-31 till 2017-01-10
        Console.Clear();
        Console.Write("Ange ett datum: [åååå-mm-dd] \n");
        string input = Console.ReadLine();
        if (DateTime.TryParse(input, out DateTime searchedDate))
        {
            var selectedDateBySearch = Helper.GetStatistics(data,location).FirstOrDefault(x => x.Date == searchedDate);

            if (selectedDateBySearch !=null)
            {

                Console.WriteLine($"Statistik enligt input: {searchedDate:yyyy-MM-dd}");
                Console.WriteLine($"Medeltemperatur: {selectedDateBySearch.AverageTemperature:F1} C°");
                Console.WriteLine($"Medelfuktighet: {selectedDateBySearch.AverageHumidity:F1}%");
            }
            else
            {
                Console.WriteLine("Det finns ingen data på valt datum! ");
            }
        }
        else
        {
            Console.WriteLine("Du skrev felaktigt format. Prova 'åååå-mm-dd' ! ");
        }

        Helper.PressToContinue("Tryck på valfri tangent för att fortsätta...");
    }
    
    public static void WarmestToColdest(List<WeatherDataProperties> data, string location)
    {
        Console.Clear();
        var fullResult = Helper.GetStatistics(data, location).OrderByDescending(x => x.AverageTemperature);
     
        Console.WriteLine($"Medeltemperaturen och Datum ({location}):");
        foreach (var item in fullResult)
        {
            Console.WriteLine(
                $"{item.Date:yyyy-MM-dd} {item.AverageTemperature:F1}°C - {item.Location}");
        }

        Helper.PressToContinue("Tryck på valfri tangent för att fortsätta...");
    }
    
    public static void DryestToMoistiest(List<WeatherDataProperties> data, string location)
    {
        Console.Clear();
        var fullResult = Helper.GetStatistics(data, location).OrderBy(x => x.AverageHumidity);

        Console.WriteLine("Medelfuktighet och Datum");
        foreach (var item in fullResult)
        {
            Console.WriteLine($"{item.Date:yyyy-MM-dd} {item.AverageHumidity:F0}");
        }

        Helper.PressToContinue("Tryck på valfri tangent för att fortsätta...");
    }
    
    public static void SortByMoldRisk(List<WeatherDataProperties> data,string location)
    {
        Console.Clear();
        var fullResult = Helper.GetStatistics(data,location).OrderByDescending(x => x.AverageMold);
        
        Console.WriteLine("Moldrisk from bottom to top and date");
        foreach (var item in fullResult)
        {
            Console.WriteLine($"{item.Date:yyyy-MM-dd} {item.AverageMold:F1}");
        }

        Helper.PressToContinue("Tryck på valfri tangent för att fortsätta...");
        
    }
    
    public static void CheckForStreakBelowTemperature(List<WeatherDataProperties> data,string location,double temperature)
    {
         Console.Clear();
         var fullResult = Helper.GetStatistics(data, location).OrderBy(x => x.Date);
         int consecutiveDays = 0;
         DateTime? streakStartDate = null;
         foreach (var streak in fullResult)
         {
             if (streak.AverageTemperature <= temperature)
             {
                 if (consecutiveDays == 0)
                 {
                     streakStartDate = streak.Date;
                 }
                 consecutiveDays++;
                 Console.WriteLine(
                     $"{streak.Date:yyyy-MM-dd}: Dag: {consecutiveDays}, Medeltemperatur: {streak.AverageTemperature:F1}C°");
                 if (consecutiveDays == 5)
                 {
                     DateTime resultDate = streak.Date.AddDays(-4);
                     Console.WriteLine($"Vi har hittat dagen. {streakStartDate:yyyy-MM-dd} Avslutar räkningen. Streaken började vid {resultDate:yyyy-MM-dd}");
                     break;
                 }
                  
             }
             else
             {
                 consecutiveDays = 0;
                 streakStartDate = null;
             }
             
             
         }
         if (consecutiveDays < 5)
         {
             Console.WriteLine($"Ingen 5-dagarsperiod under {temperature}°C kunde hittas");
         }
         
         Helper.PressToContinue("Tryck på valfri tangent för att fortsätta...");

    }
}