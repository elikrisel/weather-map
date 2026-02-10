using System.Globalization;
using System.Text.RegularExpressions;


namespace weather_map;

//TODO: LÖSA FELMEDDELANDET FÖR ATT FÅ EXAKT TID OCH DATUM
public class LoadWeatherData
{
    private static string filePath = "../../../Files/";
    
    
    public static List<WeatherDataProperties>WeatherData(string fileName)
    {
        var weatherList = new List<WeatherDataProperties>();
        string fullPath = Path.Combine(filePath, fileName);
        
        string pattern =
            @"^(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}),(?<condition>[A-ZÅÄÖa-zåäö]+),(?<temperature>[\d.+]+),(?<moldlevel>\d{2})$"; 
        
        Regex regex = new Regex(pattern);
        
            using (StreamReader file = new StreamReader(fullPath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        try
                        {
                            DateTime date = DateTime.Parse(match.Groups["date"].Value);
                            
                            
                            
                            
                            //Filtrera bort Maj 2016 och Januari 2017
                            if ((date.Year == 2016 && date.Month == 5) || 
                                (date.Year == 2017 && date.Month == 1))
                            {
                                continue;
                            }

                            weatherList.Add(new WeatherDataProperties
                            {
                                DateAndTime =
                                    DateTime.Parse(date + " " + match.Groups["time"].Value),
                                Temperature = double.Parse(match.Groups["temperature"].Value).CompareTo(CultureInfo.InvariantCulture),
                                Condition = match.Groups["condition"].Value,
                                Humidity = int.Parse(match.Groups["moldlevel"].Value)

                            });

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + "file does not exist");
                        }
                    }
                        
                    
                }
            }
         
        
        return weatherList;
    }
}