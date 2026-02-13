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
    private static void HandleSubMenu(string location, List<WeatherDataProperties> weatherList)
    {
         var data =
             weatherList.Where(x => x.Location == location).ToList();

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
                    WarmestToColdest(weatherList, location);
                    break;
                case "3":
                    DryestToMoistiest(weatherList, location);
                    break;
                case "4":
                    SortByMoldRisk(data);
                    break;
                case "5" when location == "Ute":
                    FindMeteorologicalFall(data);
                    break;
                case "6" when location == "Ute":
                    Console.WriteLine($"Kör 6 i {location}");
                    FindMeteorlogicalWinter(data);
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
        // var fullResult = data.GroupBy(x => x.DateAndTime.Date).Select(g => new
        // {
        //     DateOnly = g.Key,
        //     AverageTemperature = g.Average(x => x.Temperature),
        //     //Lock = g.FirstOrDefault()?.Location
        // }).OrderByDescending(x => x.AverageTemperature).ToList();

        var fullResult = GetStatistics(data, location);
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
        //var fullResult = data.GroupBy(x => x.DateAndTime.Date).Select(g => new
        //{
        //    DateOnly = g.Key,
        //    Averagehumity = g.Average(x => x.Humidity),
        //}).OrderBy(x => x.Averagehumity).ToList();
        var fullResult = GetStatistics(data, location).OrderBy(x => x.AverageHumidity);

        Console.WriteLine("Medelfuktighet och Datum");
        foreach (var item in fullResult)
        {
            Console.WriteLine($"{item.Date:yyyy-MM-dd} {item.AverageHumidity:F0}");
        }

        Console.ReadKey();
    }
    private static void SortByMoldRisk(List<WeatherDataProperties> data)
    {
        var fullResult = data.GroupBy(x => x.DateAndTime.Date).Select(g => new
        {
            DateOnly = g.Key,
            AverageMoldrisk = g.Average(x => x.MoldRisk),
        }).OrderBy(x => x.AverageMoldrisk).ToList();


        Console.WriteLine("Moldrisk from bottom to top and date");
        foreach (var item in fullResult)
        {
            Console.WriteLine($"{item.DateOnly.Month} {item.AverageMoldrisk:F1}");
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
    private static void FindMeteorologicalFall(List<WeatherDataProperties> data)
    {
        Console.Clear();
        var fullResult = data.GroupBy(x => x.DateAndTime.Date).Select(g => new
        {
            Date = g.Key,
            AverageTemperature = g.Average(x => x.Temperature),
            //Reading = g.Min(x => x.DateAndTime), 
        }).OrderBy(x => x.Date).ToList();

        int consecutiveDays = 0;
        for (int i = 0; i < fullResult.Count; i++)
        {
            if (fullResult[i].AverageTemperature < 10.0)
            {
                consecutiveDays++;

                //Console.WriteLine($"{fullResult[i].Date:yyyy-MM-dd HH:mm:ss}: Day {consecutiveDays}, Avg Temp: {fullResult[i].AverageTemperature:F1}°C"); //replace Date with Reading for time instead of midnight
                Console.WriteLine(
                    $"{fullResult[i].Date:yyyy-MM-dd}: Dag: {consecutiveDays}, Medeltemperatur: {fullResult[i].AverageTemperature:F1}C°");
                if (consecutiveDays == 5)
                {
                    DateTime? autumnStartDate = fullResult[i - 4].Date;
                    Console.WriteLine($"Vi har hittat dagen. {autumnStartDate:yyyy-MM-dd} Avslutar räkningen");
                    break;
                }
            }
            else
            {
                consecutiveDays = 0;
            }
        }

        Console.ReadKey();
    }
    private static void FindMeteorlogicalWinter(List<WeatherDataProperties> data)
    {
        var fullResult = data.GroupBy(x => x.DateAndTime).Select(g => new
        {
            Date = g.Key,
            //Reading = g.Min(x => x.DateAndTime),    
            AverageTemperature = g.Average(x => x.Temperature)
        }).OrderBy(x => x.Date).ToList();

        int consecutiveDays = 0;

        for (int i = 0; i < fullResult.Count; i++)
        {
            // <= 0 = 2016-11-03:
            // < 0 = 2016-11-03:
            if (fullResult[i].AverageTemperature < 0)
            {
                consecutiveDays++;
                Console.WriteLine(
                    $"{fullResult[i].Date:yyyy-MM-dd}: Dag: {consecutiveDays}, Medeltemperatur: {fullResult[i].AverageTemperature:F1}C°");
                if (consecutiveDays == 5)
                {
                    DateTime? winterDaytime = fullResult[i - 4].Date;
                    Console.WriteLine($"Du har nått medeltemperaturen under vintern: {winterDaytime} !");
                    break;
                }
            }
            else
            {
                consecutiveDays = 0;
            }
        }

        Console.ReadKey();
    }
    
    
    public static List<Statistics> GetStatistics(List<WeatherDataProperties> data, string location)
    {
        return
            data.Where(w => w.Location == location).GroupBy(x => x.DateAndTime.Date)
                .Select(g => new Statistics
                {
                    Date = g.Key,
                    Location = location,
                    AverageTemperature = g.Average(w => w.Temperature),
                    AverageHumidity = g.Average(w => w.Humidity),
                    AverageMold = g.Average(x => x.MoldRisk)
                    
                }).ToList();
    }
    
    
    
}

