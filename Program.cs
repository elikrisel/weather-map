using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;

namespace weather_map;

class Program
{
    static async Task Main(string[] args)
    {
        #region Commented Section for testing

        // while (true)
        // {
        //      ReadAndWriteFiles.ReadAllFiles("test.txt");
        //      Console.Write("Lägg till i listan: \t");
        //      string text = Console.ReadLine();
        //      await ReadAndWriteFiles.WriteAllFiles("list.txt", text);
        //      string text = "2016-05-31 13:58:30,Inne,24.8,42";
        //      RegexTest.TemperatureTest(text);      
        //      
        //      
        // }

        #endregion

        Console.WriteLine("Läser in data:");
        List<WeatherDataProperties> weatherDataList = LoadWeatherData.WeatherData("test.txt");
        bool isRunning = true;
        while (isRunning)
        {
            UI.DrawUI();
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    HandleSubMenu("Inne",weatherDataList);
                    break;
                case "2":
                    HandleSubMenu("Ute", weatherDataList);
                    break;
                case "3":
                    isRunning = !isRunning;
                    break;
                
            }
        }
    }
    
    //TODO: INCLUDE WEATHERPROPERTIES LIST TO ACCESS TO LINQ?
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
                    Console.WriteLine($"Kör 1 i {location}");
                    GetAverageTempForSelectedDate(data);
                    break;
                case "2":
                    Console.WriteLine($"Kör 2 i {location}");
                    WarmestToColdest(data);
                    break;
                case "3":
                    Console.WriteLine($"Kör 3 i {location}");
                    break;
                case "4":
                    Console.WriteLine($"Kör 4 i {location}");
                    break;
                case "5" when location == "Ute":
                    Console.WriteLine($"Kör 5 i {location}");
                    break;
                case "6" when location == "Ute":
                    Console.WriteLine($"Kör 6 i {location}");
                    break;
                case "0":
                    isRunningSubMenu = !isRunningSubMenu;
                    break;
                    
            }
        }
    }

    private static void WarmestToColdest(List<WeatherDataProperties> data)
    {
        var fullResult = data.GroupBy(x => x.DateAndTime.Date).Select(g => new
        {
            Date = g.Key,
            AverageTemperature = g.Average(x => x.Temperature)
        }).OrderByDescending(x => x.AverageTemperature).ToList();

        Console.WriteLine("Medeltemperaturen och Datum");
        foreach (var item in fullResult) 
        {
            //Console.WriteLine($"{item.Date} {item.Location}");
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
}