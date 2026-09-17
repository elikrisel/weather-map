

namespace weather_map;

class Program
{
    static void Main(string[] args)
    {
        //Test a new line in comment. Creating PR
        List<WeatherDataProperties> weatherDataList = LoadWeatherData.WeatherData("test.txt");
        RunMenu.RunApplication(weatherDataList);
        
    }


    
    
}