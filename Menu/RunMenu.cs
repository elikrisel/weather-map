using weather_map.Helpers;

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
                    Report.GenerateReport(weatherDataList);
                    break;
                case "4":
                    isRunning = !isRunning;
                    break;
                default:
                    Helper.ShowInvalidInputMessage(2000);
                    break;
            }
        }
    }
    private static void HandleSubMenu(string location, List<WeatherDataProperties> data)
    {
        bool isRunningSubMenu = true;
        while (isRunningSubMenu)
        {
            UI.DrawSubMenu(location);
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    SubMenu.GetAverageTempForSelectedDate(data,location);
                    break;
                case "2":
                    SubMenu.WarmestToColdest(data, location);
                    break;
                case "3":
                    SubMenu.DryestToMoistiest(data, location);
                    break;
                case "4":
                    SubMenu.SortByMoldRisk(data,location);
                    break;
                case "5" when location == "Ute":
                    SubMenu.CheckForStreakBelowTemperature(data, location,  10);
                    break;
                case "6" when location == "Ute":
                    SubMenu.CheckForStreakBelowTemperature(data, location,0);
                    break;
                case "0":
                    isRunningSubMenu = !isRunningSubMenu;
                    break;
                default:
                    Helper.ShowInvalidInputMessage(2000);
                    break;
            }
        }
    }
    
}

