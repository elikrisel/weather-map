using System.Globalization;
using System.Text.RegularExpressions;


namespace weather_map;

public class LoadWeatherData
{
    private static string filePath = "../../../Files/";
    
    private static string pattern =
        @"^(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}),(?<location>[A-ZÅÄÖa-zåäö]+),(?<temperature>[\d.-]+),(?<humidity>\d+)$";

    public static List<WeatherDataProperties> WeatherData(string fileName)
    {
        var weatherList = new List<WeatherDataProperties>();
        string fullPath = Path.Combine(filePath, fileName);
        Regex regex = new Regex(pattern);

        using (StreamReader file = new StreamReader(fullPath))
        {
            string line = file.ReadLine();
            while (line != null)
            {
                Match match = regex.Match(line);
                line = file.ReadLine();
                if (match.Success)
                {   
                     
                    string dateTimeString = $"{match.Groups["date"].Value} {match.Groups["time"].Value}";
                    
                    
                    if (DateTime.TryParse(dateTimeString, out DateTime parsedDateTime))
                    {
                        if ((parsedDateTime.Year == 2016 && parsedDateTime.Month == 5) ||
                            (parsedDateTime.Year == 2017 && parsedDateTime.Month == 1))
                            continue;
                        
                        
                        weatherList.Add(new WeatherDataProperties
                        {
                            DateAndTime = parsedDateTime,
                            Temperature = double.Parse(match.Groups["temperature"].Value, CultureInfo.InvariantCulture),
                            Location = match.Groups["location"].Value,
                            Humidity = int.Parse(match.Groups["humidity"].Value)
                             
                        });
                    }
                }
            }
        }
        
        return weatherList;
    }
}