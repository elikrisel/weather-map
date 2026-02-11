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
                    HandleSubMenu("Inomhus");
                    break;
                case "2":
                    HandleSubMenu("Utomhus");
                    break;
                case "3":
                    isRunning = !isRunning;
                    break;
                
            }
        }
    }

    private static void HandleSubMenu(string location)
    {
        Console.Clear();
        Console.WriteLine($"===Meny för {location}===");
        Console.WriteLine("1) Leta efter medeltemperatur för valt datum ");
        Console.WriteLine("2) Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
        Console.WriteLine("3) Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
        Console.WriteLine("4) Sortering av minst till störst risk av mögel");
        
        //Lägger på ytterligare alternativ när användaren väljer utomhus
        if (location == "Utomhus")
        {
            Console.WriteLine("5) Datum för meteorologisk Höst");
            Console.WriteLine("6) Datum för meteologisk Vinter");    
        }
        
        Console.WriteLine("\n0) Tillbaka");
        Console.WriteLine("\n Välj ett alternativ: ");

        bool isRunningSubMenu = true;
        while (isRunningSubMenu)
        {
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
                case "5" when location == "Utomhus":
                    Console.WriteLine($"Kör 5 i {location}");
                    break;
                case "6" when location == "Utomhus":
                    Console.WriteLine($"Kör 6 i {location}");
                    break;
                case "0":
                    isRunningSubMenu = !isRunningSubMenu;
                    break;
                default:
                    Console.WriteLine(
                        "Ogiltigt val i menyn. Välj mellan alternativen för att navigera och 0 för att gå tillbaka!");
                    break;
            }
        }
    }
}