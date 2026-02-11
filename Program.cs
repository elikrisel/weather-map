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
                    HandleSubMenu("Inne");
                    break;
                case "2":
                    HandleSubMenu("Ute");
                    break;
                case "3":
                    isRunning = !isRunning;
                    break;
                
            }
        }
    }
    
    //TODO: INCLUDE WEATHERPROPERTIES LIST TO ACCESS TO LINQ?
    private static void HandleSubMenu(string location)
    {
        bool isRunningSubMenu = true;
        while (isRunningSubMenu)
        {
            UI.DrawSubMenu(location);
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine($"Kör 1 i {location}");
                    break;
                case "2":
                    Console.WriteLine($"Kör 2 i {location}");
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
}