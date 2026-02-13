using System.Text.RegularExpressions;
using System.Linq;
using System.Globalization;
using System.Security.Cryptography;
using weather_map.Models;

namespace weather_map;

class Program
{
    static async Task Main(string[] args)
    {
        
        List<WeatherDataProperties> weatherDataList = LoadWeatherData.WeatherData("test.txt");
        RunMenu.RunApplication(weatherDataList);
        
    }


    
    
}