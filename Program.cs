

namespace weather_map;

class Program
{
    static void Main(string[] args)
    {
        
        List<WeatherDataProperties> weatherDataList = LoadWeatherData.WeatherData("test.txt");
        RunMenu.RunApplication(weatherDataList);
        
    }


    
    
}