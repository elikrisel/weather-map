using System.Text.RegularExpressions;

namespace weather_map;

//TODO: Skapa en List metod med WeatherDataProperties
public class LoadWeatherData
{
    private static string filePath = "../../../Files/";
    
    
    public static List<WeatherDataProperties>WeatherData(string fileName)
    {
        var weatherList = new List<WeatherDataProperties>();
        string fullPath = Path.GetFullPath(Path.Combine(filePath, fileName));
        Console.WriteLine(fullPath);
        string pattern =
            @"^(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}),(?<condition>[A-ZÅÄÖa-zåäö]+),(?<temperature>[\d.+]+),(?<moldlevel>\d{2})$"; 
        
        Regex regex = new Regex(pattern);
        try
        {
            using (StreamReader file = new StreamReader(fullPath))
            {
                string line = file.ReadLine();
                int rowCount = 0;
                while (line != null)
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        DateTime date = DateTime.Parse(match.Groups["date"].Value);
                        if (date.Year == 2016 && date.Month == 5 || date.Year == 2017 && date.Month == 1)
                        {
                            
                        }
                    }
                    
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message + "textfile does not exist. ");
        }
        
        return weatherList;
        
        
    }
}