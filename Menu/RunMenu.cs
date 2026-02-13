using weather_map.Helpers;
using weather_map.Models;

namespace weather_map;

public class RunMenu
{
    public static void RunApplication(List<WeatherDataProperties> weatherDataList)
    {
        bool isRunning = true;
        while (isRunning)
        {
            UI.DrawUI();
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    HandleSubMenu("Inne", weatherDataList);
                    break;
                case "2":
                    HandleSubMenu("Ute", weatherDataList);
                    break;
                case "3":
                    Raport.GenerateReport(weatherDataList);
                    break;
                case "4":
                    isRunning = !isRunning;
                    break;
            }
        }
    }
    private static void HandleSubMenu(string location, List<WeatherDataProperties> weatherDataList)
    {
         var data =
             weatherDataList.Where(x => x.Location == location).ToList();

        bool isRunningSubMenu = true;
        while (isRunningSubMenu)
        {
            UI.DrawSubMenu(location);
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    GetAverageTempForSelectedDate(data);
                    break;
                case "2":
                    WarmestToColdest(weatherDataList, location);
                    break;
                case "3":
                    DryestToMoistiest(weatherDataList, location);
                    break;
                case "4":
                    SortByMoldRisk(weatherDataList,location);
                    break;
                case "5" when location == "Ute":
                    CheckForStreakBelowTemperature(data, location,  10);
                    break;
                case "6" when location == "Ute":
                    CheckForStreakBelowTemperature(data, location,0);
                    break;
                case "0":
                    isRunningSubMenu = !isRunningSubMenu;
                    break;
            }
        }
    }
    private static void GetAverageTempForSelectedDate(List<WeatherDataProperties> data)
    {
        // Välj Datum mellan 2016-05-31 till 2017-01-10
        Console.Clear();
        Console.Write("Ange ett datum: [åååå-mm-dd] \n");
        string input = Console.ReadLine();
        if (DateTime.TryParse(input, out DateTime searchedDate))
        {
            var selectedDateBySearch = data.Where(x =>
                x.DateAndTime.Date == searchedDate.Date).ToList();

            if (selectedDateBySearch.Any())
            {
                double averageTemperature = selectedDateBySearch.Average(x => x.Temperature);
                double averageHumidity = selectedDateBySearch.Average(x => x.Humidity);

                Console.WriteLine($"Statistik enligt input: {searchedDate:yyyy-MM-dd}");
                Console.WriteLine($"Medeltemperatur: {averageTemperature:F1} C°");
                Console.WriteLine($"Medelfuktighet: {averageHumidity:F1}%");
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

        Console.WriteLine("Tryck på valfri tangent för att fortsätta... ");
        Console.ReadKey();
    }
    private static void WarmestToColdest(List<WeatherDataProperties> data, string location)
    {
        #region Old solution before Refactoring - REMOVE LATER
        // var fullResult = data.GroupBy(x => x.DateAndTime.Date).Select(g => new
        // {
        //     DateOnly = g.Key,
        //     AverageTemperature = g.Average(x => x.Temperature),
        //     //Lock = g.FirstOrDefault()?.Location
        // }).OrderByDescending(x => x.AverageTemperature).ToList();
        #endregion
        Console.Clear();
        var fullResult = Helper.GetStatistics(data, location).OrderByDescending(x => x.AverageTemperature);
        Console.WriteLine($"Medeltemperaturen och Datum ({location}):");
        foreach (var item in fullResult)
        {
            Console.WriteLine(
                $"{item.Date:yyyy-MM-dd} {item.AverageTemperature:F1}°C - {item.Location}");
        }

        Console.ReadKey();
    }
    private static void DryestToMoistiest(List<WeatherDataProperties> data, string location)
    {
        #region Old solution before Refactoring - REMOVE LATER
        //var fullResult = data.GroupBy(x => x.DateAndTime.Date).Select(g => new
        //{
        //    DateOnly = g.Key,
        //    Averagehumity = g.Average(x => x.Humidity),
        //}).OrderBy(x => x.Averagehumity).ToList();
        #endregion
        Console.Clear();
        var fullResult = Helper.GetStatistics(data, location).OrderBy(x => x.AverageHumidity);

        Console.WriteLine("Medelfuktighet och Datum");
        foreach (var item in fullResult)
        {
            Console.WriteLine($"{item.Date:yyyy-MM-dd} {item.AverageHumidity:F0}");
        }

        Console.ReadKey();
    }
    private static void SortByMoldRisk(List<WeatherDataProperties> data,string location)
    {
        #region Old solution before Refactoring - REMOVE LATER
        //var fullResult = data.GroupBy(x => x.DateAndTime.Date).Select(g => new
        //{
        //    DateOnly = g.Key,
        //    AverageMoldrisk = g.Average(x => x.MoldRisk),
        //}).OrderByDescending(x => x.AverageMoldrisk).ToList();
        #endregion
        Console.Clear();
        var fullResult = Helper.GetStatistics(data,location).OrderByDescending(x => x.AverageMold);
        
        Console.WriteLine("Moldrisk from bottom to top and date");
        foreach (var item in fullResult)
        {
            Console.WriteLine($"{item.Date:yyyy-MM-dd} {item.AverageMold:F1}");
        }

        Console.ReadKey();

        #region Förklaring till varför vi får 0 i inomhusmätning

        // 1. Filtrera fram alla mätningar som gjorts "Inne"
        //var inneData = data.Where(m => m.Location == "Inne");
        //
        //// 2. Kolla om någon av dessa har en luftfuktighet över 78%
        //bool finnsDetRisk = inneData.Any(m => m.Humidity > 78);
        //
        //if (finnsDetRisk)
        //{
        //    var maxRisk = inneData.Max(m => m.MoldRisk);
        //    Console.WriteLine($"Ja, det finns mätningar med mögelrisk! Max risk inne är: {maxRisk}%");
        //}
        //else
        //{
        //    var maxFuktInne = inneData.Max(m => m.Humidity);
        //    Console.WriteLine($"Nej, det finns ingen mögelrisk inomhus i denna fil.");
        //    Console.WriteLine($"Högsta luftfuktighet som uppmättes inne var {maxFuktInne}%, vilket är under gränsen på 78%.");
        //}

        #endregion
    }
    private static void FindMeteorologicalFall(List<WeatherDataProperties> data,string location)
    {
        Console.Clear();
        #region Old solution before Refactoring - REMOVE LATER
        //var fullResult = data.GroupBy(x => x.DateAndTime.Date).Select(g => new
        //{
        //    Date = g.Key,
        //    AverageTemperature = g.Average(x => x.Temperature),
        //    //Reading = g.Min(x => x.DateAndTime), 
        //}).OrderBy(x => x.Date).ToList();
        //int consecutiveDays = 0;
        // for (int i = 0; i < fullResult.Count; i++)
        //  {
        //      if (fullResult[i].AverageTemperature < 10.0)
        //      {
        //          consecutiveDays++;
        //
        //          //Console.WriteLine($"{fullResult[i].Date:yyyy-MM-dd HH:mm:ss}: Day {consecutiveDays}, Avg Temp: {fullResult[i].AverageTemperature:F1}°C"); //replace Date with Reading for time instead of midnight
        //          Console.WriteLine(
        //              $"{fullResult[i].Date:yyyy-MM-dd}: Dag: {consecutiveDays}, Medeltemperatur: {fullResult[i].AverageTemperature:F1}C°");
        //          if (consecutiveDays == 5)
        //          {
        //              DateTime? autumnStartDate = fullResult[i - 4].Date;
        //              Console.WriteLine($"Vi har hittat dagen. {autumnStartDate:yyyy-MM-dd} Avslutar räkningen");
        //              break;
        //          }
        //      }
        //      else
        //      {
        //          consecutiveDays = 0;
        //      }
        // }
        #endregion
        
        Console.ReadKey();
    }
    private static void CheckForStreakBelowTemperature(List<WeatherDataProperties> data,string location,double temperature)
    {
        
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
         
         Console.ReadKey();

    }
    
}

